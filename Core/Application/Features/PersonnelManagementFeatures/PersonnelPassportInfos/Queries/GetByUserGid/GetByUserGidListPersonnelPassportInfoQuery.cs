using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
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

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelPassportInfoQuery : IRequest<GetListResponse<GetByUserGidListPersonnelPassportInfoListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListPersonnelPassportInfoQueryHandler : IRequestHandler<GetByUserGidListPersonnelPassportInfoQuery, GetListResponse<GetByUserGidListPersonnelPassportInfoListItemDto>>
        {
            private readonly IPersonnelPassportInfoReadRepository _personnelPassportInfoReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelPassportInfo, GetByUserGidListPersonnelPassportInfoListItemDto> _noPagination;

            public GetByUserGidListPersonnelPassportInfoQueryHandler(IPersonnelPassportInfoReadRepository personnelPassportInfoReadRepository, IMapper mapper, NoPagination<X.PersonnelPassportInfo, GetByUserGidListPersonnelPassportInfoListItemDto> noPagination)
            {
                _personnelPassportInfoReadRepository = personnelPassportInfoReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelPassportInfoListItemDto>> Handle(GetByUserGidListPersonnelPassportInfoQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidPersonnelFK == request.UserGid,
                        includes: new Expression<Func<PersonnelPassportInfo, object>>[]
                        {
                       x => x.UserFK,
                        });
                }


                IPaginate<X.PersonnelPassportInfo> personnelPassportInfos = await _personnelPassportInfoReadRepository.GetListAllAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    include: x => x.Include(x => x.UserFK)
                );

                GetListResponse<GetByUserGidListPersonnelPassportInfoListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelPassportInfoListItemDto>>(personnelPassportInfos);
                return response;
            }
        }
    }
}
