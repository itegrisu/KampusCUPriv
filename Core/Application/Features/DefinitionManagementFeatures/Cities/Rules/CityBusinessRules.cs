using Application.Features.DefinitionManagementFeatures.Cities.Constants;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Cities.Rules;

public class CityBusinessRules : BaseBusinessRules
{
    private readonly ICityReadRepository _cityReadRepository;
    private readonly ICountryReadRepository _countryReadRepository;

    public CityBusinessRules(ICityReadRepository cityReadRepository, ICountryReadRepository countryReadRepository)
    {
        _cityReadRepository = cityReadRepository;
        _countryReadRepository = countryReadRepository;
    }

    public async Task CityShouldExistWhenSelected(X.City? item)
    {
        if (item == null)
            throw new BusinessException(CitiesBusinessMessages.CityNotExists);
    }

    public async Task CheckCityNameIsUnique(string cityName, Guid? cityGuid = null)
    {
        var city = await _cityReadRepository.GetAsync(predicate: x => x.SehirAdi.ToLower() == cityName.ToLower() && (cityGuid == null || x.Gid != cityGuid));
        if (city != null)
            throw new BusinessException(CitiesBusinessMessages.CityIsAlreadyExists);
    }

    public async Task CountryShouldExistWhenSelected(Guid countryGuid)
    {
        var country = await _countryReadRepository.GetAsync(predicate: x => x.Gid == countryGuid);
        if (country == null)
            throw new BusinessException(CitiesBusinessMessages.CountryNotExists);
    }

}