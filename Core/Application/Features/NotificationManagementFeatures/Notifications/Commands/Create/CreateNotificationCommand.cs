using Application.Features.NotificationManagementFeatures.Notifications.Constants;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Application.Features.NotificationManagementFeatures.Notifications.Rules;
using Application.Repositories.NotificationManagementRepos;
using AutoMapper;
using Core.Enum;
using Domain.Entities.NotificationManagements;
using MediatR;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.Create;

public class CreateNotificationCommand : IRequest<CreatedNotificationResponse>
{
    public Guid GidUserFK { get; set; }
    public string Title { get; set; }
    public ProcessType ProcessType { get; set; }
    public DateTime? ReadingDate { get; set; }
    public string? ReadingIp { get; set; }
    public string Content { get; set; }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreatedNotificationResponse>
    {
        private readonly IMapper _mapper;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly NotificationBusinessRules _notificationBusinessRules;

        public CreateNotificationCommandHandler(IMapper mapper, INotificationWriteRepository notificationWriteRepository,
                                         NotificationBusinessRules notificationBusinessRules, INotificationReadRepository notificationReadRepository)
        {
            _mapper = mapper;
            _notificationWriteRepository = notificationWriteRepository;
            _notificationBusinessRules = notificationBusinessRules;
            _notificationReadRepository = notificationReadRepository;
        }

        public async Task<CreatedNotificationResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);

            Notification notification = _mapper.Map<Notification>(request);

            await _notificationWriteRepository.AddAsync(notification);
            await _notificationWriteRepository.SaveAsync();

            Notification SavedNotification = await _notificationReadRepository.GetAsync(predicate: x => x.Gid == notification.Gid);
            GetByGidNotificationResponse obj = _mapper.Map<GetByGidNotificationResponse>(SavedNotification);

            return new()
            {
                Title = NotificationsBusinessMessages.ProcessCompleted,
                Message = NotificationsBusinessMessages.SuccessCreatedNotificationMessage,
                IsValid = true,
                Obj = obj,
            };
        }
    }
}