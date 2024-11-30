using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByCityGid
{
    public class GetByCityGidListDistrictQuery : IRequest<GetListResponse<GetByCityGidListDistrictListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid CityGid { get; set; }
        public class GetByCityGidListDistrictQueryHandler : IRequestHandler<GetByCityGidListDistrictQuery, GetListResponse<GetByCityGidListDistrictListItemDto>>
        {
            private readonly IDistrictReadRepository _districtReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.District, GetByCityGidListDistrictListItemDto> _noPagination;

            public GetByCityGidListDistrictQueryHandler(IDistrictReadRepository districtReadRepository, IMapper mapper, NoPagination<X.District, GetByCityGidListDistrictListItemDto> noPagination)
            {
                _districtReadRepository = districtReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCityGidListDistrictListItemDto>> Handle(GetByCityGidListDistrictQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidCityFK == request.CityGid,
                        includes: new Expression<Func<District, object>>[]
                        {
                       x => x.CityFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.District> districts = await _districtReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidCityFK == request.CityGid,
                    include: x => x.Include(x => x.CityFK)
                );

                GetListResponse<GetByCityGidListDistrictListItemDto> response = _mapper.Map<GetListResponse<GetByCityGidListDistrictListItemDto>>(districts);
                return response;
            }
        }
    }
}
