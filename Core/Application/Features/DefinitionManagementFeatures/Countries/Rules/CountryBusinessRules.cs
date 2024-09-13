using Application.Features.DefinitionManagementFeatures.Countries.Constants;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Countries.Rules;

public class CountryBusinessRules : BaseBusinessRules
{
    public string UlkeAdi { get; set; }
    private readonly ICountryReadRepository _countryReadRepository;

    public CountryBusinessRules(ICountryReadRepository countryReadRepository)
    {
        _countryReadRepository = countryReadRepository;
    }

    public async Task CountryShouldExistWhenSelected(X.Country? item)
    {
        if (item == null)
            throw new BusinessException(CountriesBusinessMessages.CountryNotExists);
    }

    public async Task CountryNameIsUnique(string countryName, Guid? countryGuid = null)
    {
        var country = await _countryReadRepository.GetAsync(predicate: x => x.UlkeAdi.ToLower() == countryName.ToLower() && (countryGuid == null || x.Gid != countryGuid));
        if (country != null)
            throw new BusinessException(CountriesBusinessMessages.CountryIsAlreadyExists);
    }


}