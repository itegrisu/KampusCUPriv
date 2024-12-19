using Application.Features.NotificationManagementFeatures.Notifications.Constants;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Application.Features.NotificationManagementFeatures.Notifications.Rules;
using Application.Repositories.NotificationManagementRepos;
using AutoMapper;
using Core.Enum;
using Domain.Entities.NotificationManagements;
using MediatR;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.Update;

public class UpdateNotificationCommand : IRequest<UpdatedNotificationResponse>
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string Title { get; set; }
    public ProcessType ProcessType { get; set; }
    public DateTime? ReadingDate { get; set; }
    public string? ReadingIp { get; set; }
    public string Content { get; set; }

    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, UpdatedNotificationResponse>
    {
        private readonly IMapper _mapper;
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly NotificationBusinessRules _notificationBusinessRules;

        public UpdateNotificationCommandHandler(IMapper mapper, INotificationReadRepository notificationReadRepository, INotificationWriteRepository notificationWriteRepository, NotificationBusinessRules notificationBusinessRules)
        {
            _mapper = mapper;
            _notificationReadRepository = notificationReadRepository;
            _notificationWriteRepository = notificationWriteRepository;
            _notificationBusinessRules = notificationBusinessRules;
        }

        public async Task<UpdatedNotificationResponse> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationBusinessRules.NotificationShouldExistWhenSelected(request.Gid);
            await _notificationBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);

            Notification? notification = await _notificationReadRepository.GetAsync(predicate: n => n.Gid == request.Gid, cancellationToken: cancellationToken);
            notification = _mapper.Map(request, notification);

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