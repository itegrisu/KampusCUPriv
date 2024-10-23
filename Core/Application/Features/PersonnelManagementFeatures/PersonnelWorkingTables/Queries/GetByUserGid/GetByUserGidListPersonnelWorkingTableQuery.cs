using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelWorkingTableQuery : IRequest<GetListResponse<GetByUserGidListPersonnelWorkingTableListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListPersonnelWorkingTableQueryHandler : IRequestHandler<GetByUserGidListPersonnelWorkingTableQuery, GetListResponse<GetByUserGidListPersonnelWorkingTableListItemDto>>
        {
            private readonly IPersonnelWorkingTableReadRepository _personnelWorkingTableReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelWorkingTable, GetByUserGidListPersonnelWorkingTableListItemDto> _noPagination;

            public GetByUserGidListPersonnelWorkingTableQueryHandler(IPersonnelWorkingTableReadRepository personnelWorkingTableReadRepository, IMapper mapper, NoPagination<X.PersonnelWorkingTable, GetByUserGidListPersonnelWorkingTableListItemDto> noPagination)
            {
                _personnelWorkingTableReadRepository = personnelWorkingTableReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelWorkingTableListItemDto>> Handle(GetByUserGidListPersonnelWorkingTableQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                         predicate: x => x.GidPersonnelFK == request.UserGid,
                         includes: new Expression<Func<PersonnelWorkingTable, object>>[]
                       {
                        x => x.UserFK,
                       });
                }


                IPaginate<X.PersonnelWorkingTable> personnelWorkingTables = await _personnelWorkingTableReadRepository.GetListAllAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    include: x => x.Include(x => x.UserFK)
                );

                GetListResponse<GetByUserGidListPersonnelWorkingTableListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelWorkingTableListItemDto>>(personnelWorkingTables);
                return response;
            }
        }
    }
}
