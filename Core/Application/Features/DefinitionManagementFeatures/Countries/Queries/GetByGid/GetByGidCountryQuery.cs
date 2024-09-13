using Application.Features.DefinitionManagementFeatures.Countries.Rules;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid
{
    public class GetByGidCountryQuery : IRequest<GetByGidCountryResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidCountryQueryHandler : IRequestHandler<GetByGidCountryQuery, GetByGidCountryResponse>
        {
            private readonly IMapper _mapper;
            private readonly ICountryReadRepository _countryReadRepository;
            private readonly CountryBusinessRules _countryBusinessRules;

            public GetByGidCountryQueryHandler(IMapper mapper, ICountryReadRepository countryReadRepository, CountryBusinessRules countryBusinessRules)
            {
                _mapper = mapper;
                _countryReadRepository = countryReadRepository;
                _countryBusinessRules = countryBusinessRules;
            }

            public async Task<GetByGidCountryResponse> Handle(GetByGidCountryQuery request, CancellationToken cancellationToken)
            {
                X.Country? country = await _countryReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _countryBusinessRules.CountryShouldExistWhenSelected(country);

                GetByGidCountryResponse response = _mapper.Map<GetByGidCountryResponse>(country);
                return response;
            }
        }
    }
}