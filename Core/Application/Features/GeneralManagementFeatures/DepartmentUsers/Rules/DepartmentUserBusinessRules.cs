using Application.Features.GeneralManagementFeatures.DepartmentUsers.Constants;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Rules;

public class DepartmentUserBusinessRules : BaseBusinessRules
{


    private readonly IUserReadRepository _userReadRepository;
    private readonly IDepartmentReadRepository _departmentReadRepository;
    private readonly IDepartmentUserReadRepository _departmentUserReadRepository;

    public DepartmentUserBusinessRules(IUserReadRepository userReadRepository, IDepartmentReadRepository departmentReadRepository, IDepartmentUserReadRepository departmentUserReadRepository)
    {
        _userReadRepository = userReadRepository;
        _departmentReadRepository = departmentReadRepository;
        _departmentUserReadRepository = departmentUserReadRepository;
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

    public async Task PersonelShouldNotBeAssignedToDepartmentBefore(Guid personelGid, Guid departmentGid)
    {
        var existingDepartmentUser = await _departmentUserReadRepository.GetAsync(predicate: x => x.GidPersonnelFK == personelGid && x.GidDepartmentFK == departmentGid);

        if (existingDepartmentUser != null)
        {
            throw new BusinessException(DepartmentUsersBusinessMessages.PersonelAlreadyAddedToDepartment);
        }
    }
    public async Task CheckIfUserCanBeDeleted(Guid gid)
    {
        // Ýlk olarak departmentUser'ý getiriyoruz.
        var departmentUser = _departmentUserReadRepository.GetByGid(gid);

        if (departmentUser != null)
        {
            var adminUser = _departmentReadRepository.GetByGid(departmentUser.GidDepartmentFK);

            if (adminUser.GidMainAdminFK == departmentUser.GidPersonnelFK || adminUser.GidCoAdminFK == departmentUser.GidPersonnelFK)
            {
                throw new BusinessException(DepartmentUsersBusinessMessages.HasAdminUser);
            }
        }
    }




}