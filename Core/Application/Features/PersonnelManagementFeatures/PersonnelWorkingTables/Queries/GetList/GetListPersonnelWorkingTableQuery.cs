using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetList;

public class GetListPersonnelWorkingTableQuery : IRequest<GetListResponse<GetListPersonnelWorkingTableListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelWorkingTableQueryHandler : IRequestHandler<GetListPersonnelWorkingTableQuery, GetListResponse<GetListPersonnelWorkingTableListItemDto>>
    {
        private readonly IPersonnelWorkingTableReadRepository _personnelWorkingTableReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelWorkingTable, GetListPersonnelWorkingTableListItemDto> _noPagination;

        public GetListPersonnelWorkingTableQueryHandler(IPersonnelWorkingTableReadRepository personnelWorkingTableReadRepository, IMapper mapper, NoPagination<X.PersonnelWorkingTable, GetListPersonnelWorkingTableListItemDto> noPagination)
        {
            _personnelWorkingTableReadRepository = personnelWorkingTableReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelWorkingTableListItemDto>> Handle(GetListPersonnelWorkingTableQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                     includes: new Expression<Func<PersonnelWorkingTable, object>>[]
                   {
                        x => x.UserFK,
                   });
            }


            IPaginate<X.PersonnelWorkingTable> personnelWorkingTables = await _personnelWorkingTableReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK)
            );

            GetListResponse<GetListPersonnelWorkingTableListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelWorkingTableListItemDto>>(personnelWorkingTables);
            return response;
        }
    }
}