using Application.Features.GeneralFeatures.Admins.Constants;
using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Admins.Rules;

public class AdminBusinessRules : BaseBusinessRules
{
    private readonly IAdminReadRepository _adminReadRepository;

    public AdminBusinessRules(IAdminReadRepository adminReadRepository)
    {
        _adminReadRepository = adminReadRepository;
    }

    public async Task AdminShouldExistWhenSelected(X.Admin? item)
    {
        if (item == null)
            throw new BusinessException(AdminsBusinessMessages.AdminNotExists);
    }

    public async Task AdminEmailShouldBeUnique(string email)
    {
        var existingAdmin = await _adminReadRepository.GetSingleAsync(a => a.Email == email);
        if (existingAdmin != null)
            throw new BusinessException(AdminsBusinessMessages.EmailAlreadyExists);
    }
    public async Task AdminEmailShouldBeUniqueWhenUpdating(Guid adminId, string email)
    {
        var existingAdmin = await _adminReadRepository.GetSingleAsync(a => a.Email == email && a.Gid != adminId);
        if (existingAdmin != null)
            throw new BusinessException(AdminsBusinessMessages.EmailAlreadyExists);
    }
}
