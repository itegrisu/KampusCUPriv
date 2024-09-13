using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Rules;

public class PersonnelGraduatedSchoolBusinessRules : BaseBusinessRules
{

    private readonly IUserReadRepository _userReadRepository;

    public PersonnelGraduatedSchoolBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task PersonnelGraduatedSchoolShouldExistWhenSelected(X.PersonnelGraduatedSchool? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelGraduatedSchoolsBusinessMessages.PersonnelGraduatedSchoolNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gid)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (user == null)
            throw new BusinessException(PersonnelGraduatedSchoolsBusinessMessages.UserNotExists);
    }
}