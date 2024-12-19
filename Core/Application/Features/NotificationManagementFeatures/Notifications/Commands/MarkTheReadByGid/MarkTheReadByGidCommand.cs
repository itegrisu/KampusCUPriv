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

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.MarkTheReadByGid;

public class MarkTheReadByGidCommand : IRequest<MarkTheReadByGidResponse>
{
    public Guid Gid { get; set; }


    public class MarkTheReadByGidCommandHandler : IRequestHandler<MarkTheReadByGidCommand, MarkTheReadByGidResponse>
    {
        private readonly IMapper _mapper;
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly GetUserInfo _getUserInfo;
        private readonly NotificationBusinessRules _notificationBusinessRules;

        public MarkTheReadByGidCommandHandler(IMapper mapper, INotificationReadRepository notificationReadRepository, INotificationWriteRepository notificationWriteRepository, NotificationBusinessRules notificationBusinessRules, GetUserInfo getUserInfo)
        {
            _mapper = mapper;
            _notificationReadRepository = notificationReadRepository;
            _notificationWriteRepository = notificationWriteRepository;
            _notificationBusinessRules = notificationBusinessRules;
            _getUserInfo = getUserInfo;
        }

        public async Task<MarkTheReadByGidResponse> Handle(MarkTheReadByGidCommand request, CancellationToken cancellationToken)
        {
            await _notificationBusinessRules.NotificationShouldExistWhenSelected(request.Gid);
            

            Notification? notification = await _notificationReadRepository.GetAsync(predicate: n => n.Gid == request.Gid, cancellationToken: cancellationToken);
            notification.ReadingIp = notification.ReadingIp == null ? _getUserInfo.GetUserIpAddress() : notification.ReadingIp;
            notification.ReadingDate =notification.ReadingDate == null ? DateTime.Now : notification.ReadingDate;

            _notificationWriteRepository.Update(notification!);
            await _notificationWriteRepository.SaveAsync();

            GetByGidNotificationResponse obj = _mapper.Map<GetByGidNotificationResponse>(notification);

            return new()
            {
                Title = NotificationsBusinessMessages.ProcessCompleted,
                Message = NotificationsBusinessMessages.SuccessUpdatedNotificationMessage,
                IsValid = true,
                Obj = obj   
            };
        }
    }
}