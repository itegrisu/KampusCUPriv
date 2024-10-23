using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
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

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelPermitInfoQuery : IRequest<GetListResponse<GetByUserGidListPersonnelPermitInfoListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListPersonnelPermitInfoQueryHandler : IRequestHandler<GetByUserGidListPersonnelPermitInfoQuery, GetListResponse<GetByUserGidListPersonnelPermitInfoListItemDto>>
        {
            private readonly IPersonnelPermitInfoReadRepository _personnelPermitInfoReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelPermitInfo, GetByUserGidListPersonnelPermitInfoListItemDto> _noPagination;

            public GetByUserGidListPersonnelPermitInfoQueryHandler(IPersonnelPermitInfoReadRepository personnelPermitInfoReadRepository, IMapper mapper, NoPagination<X.PersonnelPermitInfo, GetByUserGidListPersonnelPermitInfoListItemDto> noPagination)
            {
                _personnelPermitInfoReadRepository = personnelPermitInfoReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelPermitInfoListItemDto>> Handle(GetByUserGidListPersonnelPermitInfoQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       predicate: x => x.GidPersonnelFK == request.UserGid,
                       includes: new Expression<Func<PersonnelPermitInfo, object>>[]
                       {
                       x => x.UserFK,
                       x=> x.PermitTypeFK
                       });
                }


                IPaginate<X.PersonnelPermitInfo> personnelPermitInfos = await _personnelPermitInfoReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    include: x => x.Include(x => x.UserFK).Include(x => x.PermitTypeFK)
                );

                GetListResponse<GetByUserGidListPersonnelPermitInfoListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelPermitInfoListItemDto>>(personnelPermitInfos);
                return response;
            }
        }
    }
}
