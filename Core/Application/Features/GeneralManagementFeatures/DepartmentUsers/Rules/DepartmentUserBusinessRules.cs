using Application.Features.GeneralManagementFeatures.DepartmentUsers.Constants;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Rules;

public class DepartmentUserBusinessRules : BaseBusinessRules
{


    private readonly IUserReadRepository _userReadRepository;
    private readonly IDepartmentReadRepository _departmentReadRepository;

    public DepartmentUserBusinessRules(IUserReadRepository userReadRepository, IDepartmentReadRepository departmentReadRepository)
    {
        _userReadRepository = userReadRepository;
        _departmentReadRepository = departmentReadRepository;
    }

    public async Task DepartmentUserShouldExistWhenSelected(X.DepartmentUser? item)
    {
        if (item == null)
            throw new BusinessException(DepartmentUsersBusinessMessages.DepartmentUserNotExists);
    }

    public async Task DepartmantShouldExistWhenSelected(Guid departmantGid)
    {
        var item = await _departmentReadRepository.GetAsync(predicate: x => x.Gid == departmantGid);

        if (item == null)
            throw new BusinessException(DepartmentUsersBusinessMessages.DepartmentNotExists);
    }

    public async Task PersonelShouldExistWhenSelected(Guid personelGid)
    {
        var item = await _userReadRepository.GetAsync(predicate: x => x.Gid == personelGid);

        if (item == null)
            throw new BusinessException(DepartmentUsersBusinessMessages.PersonelNotExists);
    }

}