using Application.Features.GeneralFeatures.Admins.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Admins.Rules;

public class AdminBusinessRules : BaseBusinessRules
{
    public async Task AdminShouldExistWhenSelected(X.Admin? item)
    {
        if (item == null)
            throw new BusinessException(AdminsBusinessMessages.AdminNotExists);
    }
}