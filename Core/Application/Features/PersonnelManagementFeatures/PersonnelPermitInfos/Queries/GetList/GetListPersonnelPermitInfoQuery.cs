using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetList;

public class GetListPersonnelPermitInfoQuery : IRequest<GetListResponse<GetListPersonnelPermitInfoListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelPermitInfoQueryHandler : IRequestHandler<GetListPersonnelPermitInfoQuery, GetListResponse<GetListPersonnelPermitInfoListItemDto>>
    {
        private readonly IPersonnelPermitInfoReadRepository _personnelPermitInfoReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelPermitInfo, GetListPersonnelPermitInfoListItemDto> _noPagination;

        public GetListPersonnelPermitInfoQueryHandler(IPersonnelPermitInfoReadRepository personnelPermitInfoReadRepository, IMapper mapper, NoPagination<X.PersonnelPermitInfo, GetListPersonnelPermitInfoListItemDto> noPagination)
        {
            _personnelPermitInfoReadRepository = personnelPermitInfoReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelPermitInfoListItemDto>> Handle(GetListPersonnelPermitInfoQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<PersonnelPermitInfo, object>>[]
                   {
                       x => x.UserFK,
                       x=> x.PermitTypeFK
                   });
            }


            IPaginate<X.PersonnelPermitInfo> personnelPermitInfos = await _personnelPermitInfoReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK).Include(x => x.PermitTypeFK)
            );

            GetListResponse<GetListPersonnelPermitInfoListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelPermitInfoListItemDto>>(personnelPermitInfos);
            return response;
        }
    }
}