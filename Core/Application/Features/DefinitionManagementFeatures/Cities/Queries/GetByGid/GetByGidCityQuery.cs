using Application.Features.DefinitionManagementFeatures.Cities.Rules;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid
{
    public class GetByGidCityQuery : IRequest<GetByGidCityResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidCityQueryHandler : IRequestHandler<GetByGidCityQuery, GetByGidCityResponse>
        {
            private readonly IMapper _mapper;
            private readonly ICityReadRepository _cityReadRepository;
            private readonly CityBusinessRules _cityBusinessRules;

            public GetByGidCityQueryHandler(IMapper mapper, ICityReadRepository cityReadRepository, CityBusinessRules cityBusinessRules)
            {
                _mapper = mapper;
                _cityReadRepository = cityReadRepository;
                _cityBusinessRules = cityBusinessRules;
            }

            public async Task<GetByGidCityResponse> Handle(GetByGidCityQuery request, CancellationToken cancellationToken)
            {
                X.City? city = await _cityReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                    include: i => i.Include(i => i.CountryFK));

                await _cityBusinessRules.CityShouldExistWhenSelected(city);

                GetByGidCityResponse response = _mapper.Map<GetByGidCityResponse>(city);
                return response;
            }
        }
    }
}