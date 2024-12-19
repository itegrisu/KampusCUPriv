using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.NotificationManagementFeatures.Notifications.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.NotificationManagementRepos;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.NotificationManagements;

namespace Application.Features.NotificationManagementFeatures.Notifications.Rules;

public class NotificationBusinessRules : BaseBusinessRules
{
    private readonly INotificationReadRepository _notificationReadRepository;
    private readonly IUserReadRepository _userReadRepository;

    public NotificationBusinessRules(INotificationReadRepository notificationReadRepository, IUserReadRepository userReadRepository)
    {
        _notificationReadRepository = notificationReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task NotificationIdShouldExistWhenSelected(Guid gid, CancellationToken cancellationToken)
    {
        Notification? notification = await _notificationReadRepository.GetAsync(
            predicate: n => n.Gid == gid,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await NotificationShouldExistWhenSelected(notification.Gid);
    }

    public async Task NotificationShouldExistWhenSelected(Guid notificationGid)
    {
        if (await _notificationReadRepository.GetAsync(predicate: n => n.Gid == notificationGid) == null)
            throw new BusinessException(NotificationsBusinessMessages.NotificationNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        if (await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid) == null)
            throw new BusinessException(NotificationsBusinessMessages.UserNotExists);
    }

}