using Application.Helpers.PaginationHelpers;
using Application.Repositories.NotificationManagementRepos;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.NotificationManagements;
using MediatR;

namespace Application.Features.NotificationManagementFeatures.Notifications.Queries.GetList;

public class GetListNotificationQuery : IRequest<GetListResponse<GetListNotificationListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetListNotificationQueryHandler : IRequestHandler<GetListNotificationQuery, GetListResponse<GetListNotificationListItemDto>>
    {
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<Notification, GetListNotificationListItemDto> _noPagination;

        public GetListNotificationQueryHandler(INotificationReadRepository notificationReadRepository, IMapper mapper, NoPagination<Notification, GetListNotificationListItemDto> noPagination)
        {
            _notificationReadRepository = notificationReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListNotificationListItemDto>> Handle(GetListNotificationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken, predicate: x=> x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate);

            IPaginate<Notification> notifications = await _notificationReadRepository.GetListAsync(
                predicate: x => x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate,
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListNotificationListItemDto> response = _mapper.Map<GetListResponse<GetListNotificationListItemDto>>(notifications);
            return response;
        }
    }
}