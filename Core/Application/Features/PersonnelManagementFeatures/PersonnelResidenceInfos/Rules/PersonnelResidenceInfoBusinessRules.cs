using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;

public class PersonnelResidenceInfoBusinessRules : BaseBusinessRules
{

    private readonly IUserReadRepository _userReadRepository;

    public PersonnelResidenceInfoBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task PersonnelResidenceInfoShouldExistWhenSelected(X.PersonnelResidenceInfo? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelResidenceInfosBusinessMessages.PersonnelResidenceInfoNotExists);
    }

    public async Task PersonnelShouldExistWhenSelected(Guid userGid)
    {

        User user = await _userReadRepository.GetAsync(predicate: x => x.Gid == userGid);

        if (user == null)
            throw new BusinessException(PersonnelResidenceInfosBusinessMessages.PersonnelNotExists);
    }

}