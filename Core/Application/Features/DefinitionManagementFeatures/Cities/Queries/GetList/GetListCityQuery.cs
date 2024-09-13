using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetList;

public class GetListCityQuery : IRequest<GetListResponse<GetListCityListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCityQueryHandler : IRequestHandler<GetListCityQuery, GetListResponse<GetListCityListItemDto>>
    {
        private readonly ICityReadRepository _cityReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.City, GetListCityListItemDto> _noPagination;

        public GetListCityQueryHandler(ICityReadRepository cityReadRepository, IMapper mapper, NoPagination<X.City, GetListCityListItemDto> noPagination)
        {
            _cityReadRepository = cityReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListCityListItemDto>> Handle(GetListCityQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                  includes: new Expression<Func<City, object>>[]
                  {
                      x=>x.CountryFK
                  });
            }


            IPaginate<X.City> citys = await _cityReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.CountryFK));

            GetListResponse<GetListCityListItemDto> response = _mapper.Map<GetListResponse<GetListCityListItemDto>>(citys);
            return response;
        }
    }
}