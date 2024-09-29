using Application.Features.DefinitionManagementFeatures.Countries.Constants;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Countries.Rules;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;

public class CreateCountryCommand : IRequest<CreatedCountryResponse>
{

    public string Name { get; set; }
    public string CountryCode { get; set; }
    public string? PhoneCode { get; set; }

    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CreatedCountryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICountryWriteRepository _countryWriteRepository;
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly CountryBusinessRules _countryBusinessRules;

        public CreateCountryCommandHandler(IMapper mapper, ICountryWriteRepository countryWriteRepository,
                                         CountryBusinessRules countryBusinessRules, ICountryReadRepository countryReadRepository)
        {
            _mapper = mapper;
            _countryWriteRepository = countryWriteRepository;
            _countryBusinessRules = countryBusinessRules;
            _countryReadRepository = countryReadRepository;
        }

        public async Task<CreatedCountryResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            await _countryBusinessRules.CountryNameIsUnique(request.Name);
            int maxRowNo = await _countryReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Country country = _mapper.Map<X.Country>(request);
            country.RowNo = maxRowNo + 1;

            await _countryWriteRepository.AddAsync(country);
            await _countryWriteRepository.SaveAsync();

            X.Country savedCountry = await _countryReadRepository.GetAsync(predicate: x => x.Gid == country.Gid);

            GetByGidCountryResponse obj = _mapper.Map<GetByGidCountryResponse>(savedCountry);
            return new()
            {
                Title = CountriesBusinessMessages.ProcessCompleted,
                Message = CountriesBusinessMessages.SuccessCreatedCountryMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}