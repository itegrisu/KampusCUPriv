using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Constants;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Rules;

public class PersonnelPermitInfoBusinessRules : BaseBusinessRules
{
    public Guid GidPersonelFK { get; set; }
    public Guid GidPermitFK { get; set; }

    private readonly IUserReadRepository _userReadRepository;
    private readonly IPermitTypeReadRepository _permitTypeReadRepository;

    public PersonnelPermitInfoBusinessRules(IUserReadRepository userReadRepository, IPermitTypeReadRepository permitTypeReadRepository)
    {
        _userReadRepository = userReadRepository;
        _permitTypeReadRepository = permitTypeReadRepository;
    }

    public async Task PersonnelPermitInfoShouldExistWhenSelected(X.PersonnelPermitInfo? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelPermitInfosBusinessMessages.PersonnelPermitInfoNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gid)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (user == null)
            throw new BusinessException(PersonnelPermitInfosBusinessMessages.UserNotExists);
    }

    public async Task PermitTypeShouldExistWhenSelected(Guid gid)
    {
        var permitType = await _permitTypeReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (permitType == null)
            throw new BusinessException(PersonnelPermitInfosBusinessMessages.PermitTypeNotExists);
    }
}