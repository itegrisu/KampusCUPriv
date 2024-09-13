using Application.Features.DefinitionManagementFeatures.Countries.Constants;
using Application.Features.DefinitionManagementFeatures.Countries.Rules;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Delete;

public class DeleteCountryCommand : IRequest<DeletedCountryResponse>
{
	public Guid Gid { get; set; }

    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, DeletedCountryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly ICountryWriteRepository _countryWriteRepository;
        private readonly CountryBusinessRules _countryBusinessRules;

        public DeleteCountryCommandHandler(IMapper mapper, ICountryReadRepository countryReadRepository,
                                         CountryBusinessRules countryBusinessRules, ICountryWriteRepository countryWriteRepository)
        {
            _mapper = mapper;
            _countryReadRepository = countryReadRepository;
            _countryBusinessRules = countryBusinessRules;
            _countryWriteRepository = countryWriteRepository;
        }

        public async Task<DeletedCountryResponse> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            X.Country? country = await _countryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _countryBusinessRules.CountryShouldExistWhenSelected(country);
            country.DataState = Core.Enum.DataState.Deleted;

            _countryWriteRepository.Update(country);
            await _countryWriteRepository.SaveAsync();

            return new()
            {
                Title = CountriesBusinessMessages.ProcessCompleted,
                Message = CountriesBusinessMessages.SuccessDeletedCountryMessage,
                IsValid = true
            };
        }
    }
}