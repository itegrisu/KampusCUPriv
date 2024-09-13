using Application.Features.GeneralManagementFeatures.Departments.Constants;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Departments.Rules;

public class DepartmentBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IDepartmentReadRepository _departmendReadRepository;

    public DepartmentBusinessRules(IUserReadRepository userReadRepository, IDepartmentReadRepository departmendReadRepository)
    {
        _userReadRepository = userReadRepository;
        _departmendReadRepository = departmendReadRepository;
    }

    public async Task DepartmentShouldExistWhenSelected(X.Department? item)
    {
        if (item == null)
            throw new BusinessException(DepartmentsBusinessMessages.DepartmentNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gid)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (user == null)
            throw new BusinessException(DepartmentsBusinessMessages.UserNotExists);
    }

    public async Task CheckDepartmentName(string departmentName, Guid? departmentGid = null)
    {
        // Eðer departmentId null deðilse, güncelleme iþlemi yapýldýðýný anlýyoruz ve departmanýn kendisini hariç tutuyoruz
        var department = await _departmendReadRepository.GetAsync(
            predicate: x => x.DepartmanAdi == departmentName && (departmentGid == null || x.Gid != departmentGid));

        if (department != null)
        {
            throw new BusinessException(DepartmentsBusinessMessages.DepartmentAlreadyExists);
        }
    }



}