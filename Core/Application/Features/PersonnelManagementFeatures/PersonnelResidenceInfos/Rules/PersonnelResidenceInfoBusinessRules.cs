using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;

public class PersonnelResidenceInfoBusinessRules : BaseBusinessRules
{
    //public Guid GidPersonelFK { get; set; }
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

    public async Task UserShouldExistWhenSelected(Guid gidPersonelFK)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidPersonelFK);
        if (user == null)
            throw new BusinessException(PersonnelResidenceInfosBusinessMessages.UserNotExists);
    }

}