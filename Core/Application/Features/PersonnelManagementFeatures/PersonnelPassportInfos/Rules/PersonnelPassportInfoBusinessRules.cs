using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Rules;

public class PersonnelPassportInfoBusinessRules : BaseBusinessRules
{
    public Guid GidPersonelFK { get; set; }
    private readonly IUserReadRepository _userReadRepository;

    public PersonnelPassportInfoBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task PersonnelPassportInfoShouldExistWhenSelected(X.PersonnelPassportInfo? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelPassportInfosBusinessMessages.PersonnelPassportInfoNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gid)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (user == null)
            throw new BusinessException(PersonnelPassportInfosBusinessMessages.UserNotExists);
    }

}