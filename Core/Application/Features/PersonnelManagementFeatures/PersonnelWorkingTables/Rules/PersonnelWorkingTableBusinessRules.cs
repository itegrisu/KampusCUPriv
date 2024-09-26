using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Rules;

public class PersonnelWorkingTableBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;

    public PersonnelWorkingTableBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task PersonnelWorkingTableShouldExistWhenSelected(X.PersonnelWorkingTable? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelWorkingTablesBusinessMessages.PersonnelWorkingTableNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gidPersonelFK)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidPersonelFK);
        if (user == null)
            throw new BusinessException(PersonnelResidenceInfosBusinessMessages.UserNotExists);
    }
}