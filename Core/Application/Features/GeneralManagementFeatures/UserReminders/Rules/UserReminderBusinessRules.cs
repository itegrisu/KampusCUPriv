using Application.Features.GeneralManagementFeatures.UserReminders.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Rules;

public class UserReminderBusinessRules : BaseBusinessRules
{
    public async Task UserReminderShouldExistWhenSelected(X.UserReminder? item)
    {
        if (item == null)
            throw new BusinessException(UserRemindersBusinessMessages.UserReminderNotExists);
    }
}