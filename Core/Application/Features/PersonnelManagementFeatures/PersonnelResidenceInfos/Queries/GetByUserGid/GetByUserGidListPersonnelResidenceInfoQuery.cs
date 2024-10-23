using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
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

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelResidenceInfoQuery : IRequest<GetListResponse<GetByUserGidListPersonnelResidenceInfoListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid {  get; set; }
        public class GetByUserGidListPersonnelResidenceInfoQueryHandler : IRequestHandler<GetByUserGidListPersonnelResidenceInfoQuery, GetListResponse<GetByUserGidListPersonnelResidenceInfoListItemDto>>
        {
            private readonly IPersonnelResidenceInfoReadRepository _personnelResidenceInfoReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelResidenceInfo, GetByUserGidListPersonnelResidenceInfoListItemDto> _noPagination;

            public GetByUserGidListPersonnelResidenceInfoQueryHandler(IPersonnelResidenceInfoReadRepository personnelResidenceInfoReadRepository, IMapper mapper, NoPagination<X.PersonnelResidenceInfo, GetByUserGidListPersonnelResidenceInfoListItemDto> noPagination)
            {
                _personnelResidenceInfoReadRepository = personnelResidenceInfoReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelResidenceInfoListItemDto>> Handle(GetByUserGidListPersonnelResidenceInfoQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidPersonnelFK == request.UserGid,
                        includes: new Expression<Func<PersonnelResidenceInfo, object>>[]
                        {
                       x => x.UserFK,
                        });
                }


                IPaginate<X.PersonnelResidenceInfo> personnelResidenceInfos = await _personnelResidenceInfoReadRepository.GetListAllAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    include: x => x.Include(x => x.UserFK));

                GetListResponse<GetByUserGidListPersonnelResidenceInfoListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelResidenceInfoListItemDto>>(personnelResidenceInfos);
                return response;
            }
        }
    }
}
