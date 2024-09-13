using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Constants;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Rules;

public class PersonnelAddressBusinessRules : BaseBusinessRules
{


    private readonly ICityReadRepository _cityReadRepository;
    private readonly IUserReadRepository _userReadRepository;

    public PersonnelAddressBusinessRules(ICityReadRepository cityReadRepository, IUserReadRepository userReadRepository)
    {
        _cityReadRepository = cityReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task PersonnelAddressShouldExistWhenSelected(X.PersonnelAddress? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelAddressesBusinessMessages.PersonnelAddressNotExists);
    }

    public async Task CityShouldExistWhenSelected(Guid cityGuid)
    {
        var city = await _cityReadRepository.GetAsync(predicate: x => x.Gid == cityGuid);
        if (city == null)
            throw new BusinessException(PersonnelAddressesBusinessMessages.CityNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid userGuid)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == userGuid);
        if (user == null)
            throw new BusinessException(PersonnelAddressesBusinessMessages.UserNotExists);
    }
}