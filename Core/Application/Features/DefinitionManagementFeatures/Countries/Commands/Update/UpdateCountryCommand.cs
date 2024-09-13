using Application.Features.DefinitionManagementFeatures.Countries.Constants;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Countries.Rules;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Update;

public class UpdateCountryCommand : IRequest<UpdatedCountryResponse>
{
    public Guid Gid { get; set; }
    public string UlkeAdi { get; set; }
    public string UlkeKodu { get; set; }
    public string? TelefonKodu { get; set; }


    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, UpdatedCountryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICountryWriteRepository _countryWriteRepository;
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly CountryBusinessRules _countryBusinessRules;

        public UpdateCountryCommandHandler(IMapper mapper, ICountryWriteRepository countryWriteRepository,
                                         CountryBusinessRules countryBusinessRules, ICountryReadRepository countryReadRepository)
        {
            _mapper = mapper;
            _countryWriteRepository = countryWriteRepository;
            _countryBusinessRules = countryBusinessRules;
            _countryReadRepository = countryReadRepository;
        }

        public async Task<UpdatedCountryResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            X.Country? country = await _countryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _countryBusinessRules.CountryShouldExistWhenSelected(country);
            await _countryBusinessRules.CountryNameIsUnique(request.UlkeAdi, request.Gid);
            country = _mapper.Map(request, country);

            _countryWriteRepository.Update(country!);
            await _countryWriteRepository.SaveAsync();
            GetByGidCountryResponse obj = _mapper.Map<GetByGidCountryResponse>(country);

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