using Application.Features.NotificationManagementFeatures.Notifications.Rules;
using Application.Repositories.NotificationManagementRepos;
using AutoMapper;
using Domain.Entities.NotificationManagements;
using MediatR;

namespace Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;

public class GetByGidNotificationQuery : IRequest<GetByGidNotificationResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdNotificationQueryHandler : IRequestHandler<GetByGidNotificationQuery, GetByGidNotificationResponse>
    {
        private readonly IMapper _mapper;
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly NotificationBusinessRules _notificationBusinessRules;

        public GetByIdNotificationQueryHandler(IMapper mapper, INotificationReadRepository notificationReadRepository, NotificationBusinessRules notificationBusinessRules)
        {
            _mapper = mapper;
            _notificationReadRepository = notificationReadRepository;
            _notificationBusinessRules = notificationBusinessRules;
        }

        public async Task<GetByGidNotificationResponse> Handle(GetByGidNotificationQuery request, CancellationToken cancellationToken)
        {
            await _notificationBusinessRules.NotificationShouldExistWhenSelected(request.Gid);

            Notification? notification = await _notificationReadRepository.GetAsync(predicate: n => n.Gid == request.Gid, cancellationToken: cancellationToken);

            GetByGidNotificationResponse response = _mapper.Map<GetByGidNotificationResponse>(notification);
            return response;
        }
    }
}