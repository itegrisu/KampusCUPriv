using Application.Features.NotificationManagementFeatures.Notifications.Commands.Update;
using Application.Features.NotificationManagementFeatures.Notifications.Constants;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Application.Features.NotificationManagementFeatures.Notifications.Rules;
using Application.Helpers;
using Application.Repositories.NotificationManagementRepos;
using AutoMapper;
using Core.Enum;
using Domain.Entities.NotificationManagements;
using MediatR;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.MarkTheReadAllNotification;

public class MarkTheReadAllNotificationCommand : IRequest<MarkTheReadAllNotificationResponse>
{
    public Guid GidUserFK { get; set; }
    public class MarkTheReadAllNotificationCommandHandler : IRequestHandler<MarkTheReadAllNotificationCommand, MarkTheReadAllNotificationResponse>
    {
        private readonly IMapper _mapper;
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly GetUserInfo _getUserInfo;
        private readonly NotificationBusinessRules _notificationBusinessRules;

        public MarkTheReadAllNotificationCommandHandler(IMapper mapper, INotificationReadRepository notificationReadRepository, INotificationWriteRepository notificationWriteRepository, NotificationBusinessRules notificationBusinessRules, GetUserInfo getUserInfo)
        {
            _mapper = mapper;
            _notificationReadRepository = notificationReadRepository;
            _notificationWriteRepository = notificationWriteRepository;
            _notificationBusinessRules = notificationBusinessRules;
            _getUserInfo = getUserInfo;
        }

        public async Task<MarkTheReadAllNotificationResponse> Handle(MarkTheReadAllNotificationCommand request, CancellationToken cancellationToken)
        {
            List<Notification>? notifications = _notificationReadRepository.GetWhere(notifications => notifications.ReadingDate == null && notifications.GidUserFK == request.GidUserFK, false).ToList();

            if (notifications.Count > 0)
            {
                for (int i = 0; i < notifications.Count; i++)
                {
                    notifications[i].ReadingIp = _getUserInfo.GetUserIpAddress();
                    notifications[i].ReadingDate = DateTime.Now;
                    _notificationWriteRepository.Update(notifications[i]);
                    _notificationWriteRepository.Save();
                }

            }


            return new()
            {
                Title = NotificationsBusinessMessages.ProcessCompleted,
                Message = NotificationsBusinessMessages.SuccessUpdatedNotificationMessage,
                IsValid = true,

            };
        }
    }
}