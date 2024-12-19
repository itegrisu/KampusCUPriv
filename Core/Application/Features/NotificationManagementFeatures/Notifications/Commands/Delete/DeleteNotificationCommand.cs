using Application.Features.NotificationManagementFeatures.Notifications.Constants;
using Application.Features.NotificationManagementFeatures.Notifications.Rules;
using Application.Repositories.NotificationManagementRepos;
using Domain.Entities.NotificationManagements;
using MediatR;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.Delete;

public class DeleteNotificationCommand : IRequest<DeletedNotificationResponse>
{
    public Guid Gid { get; set; }

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, DeletedNotificationResponse>
    {
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly NotificationBusinessRules _notificationBusinessRules;

        public DeleteNotificationCommandHandler(INotificationReadRepository notificationReadRepository, INotificationWriteRepository notificationWriteRepository, NotificationBusinessRules notificationBusinessRules)
        {
            _notificationReadRepository = notificationReadRepository;
            _notificationWriteRepository = notificationWriteRepository;
            _notificationBusinessRules = notificationBusinessRules;
        }

        public async Task<DeletedNotificationResponse> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationBusinessRules.NotificationShouldExistWhenSelected(request.Gid);

            Notification? notification = await _notificationReadRepository.GetAsync(predicate: n => n.Gid == request.Gid, cancellationToken: cancellationToken);
            notification.DataState = Core.Enum.DataState.Deleted;

            _notificationWriteRepository.Update(notification);
            await _notificationWriteRepository.SaveAsync();

            return new()
            {
                Title = NotificationsBusinessMessages.ProcessCompleted,
                Message = NotificationsBusinessMessages.SuccessDeletedNotificationMessage,
                IsValid = true
            };
        }
    }
}