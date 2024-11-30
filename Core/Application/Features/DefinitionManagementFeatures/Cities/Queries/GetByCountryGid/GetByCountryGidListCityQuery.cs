using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
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

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByCountryGid
{
    public class GetByCountryGidListCityQuery : IRequest<GetListResponse<GetByCoıntryGidListCityListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid CountryGid { get; set; }
        public class GetByCountryGidListCityQueryHandler : IRequestHandler<GetByCountryGidListCityQuery, GetListResponse<GetByCoıntryGidListCityListItemDto>>
        {
            private readonly ICityReadRepository _cityReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.City, GetByCoıntryGidListCityListItemDto> _noPagination;

            public GetByCountryGidListCityQueryHandler(ICityReadRepository cityReadRepository, IMapper mapper, NoPagination<X.City, GetByCoıntryGidListCityListItemDto> noPagination)
            {
                _cityReadRepository = cityReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCoıntryGidListCityListItemDto>> Handle(GetByCountryGidListCityQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                      predicate: x => x.GidCountryFK == request.CountryGid,
                      includes: new Expression<Func<City, object>>[]
                      {
                      x=>x.CountryFK
                      });
                }


                IPaginate<X.City> citys = await _cityReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidCountryFK == request.CountryGid,
                    include: x => x.Include(x => x.CountryFK));

                GetListResponse<GetByCoıntryGidListCityListItemDto> response = _mapper.Map<GetListResponse<GetByCoıntryGidListCityListItemDto>>(citys);
                return response;
            }
        }
    }
}
