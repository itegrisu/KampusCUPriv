using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.NotificationManagementRepos;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.NotificationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByUserGid
{
    public class GetByUserGidQuery : IRequest<GetListResponse<GetByUserGidNotificationListItemDto>>
    {
        public Guid Gid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetByUserGidNotificationQueryHandler : IRequestHandler<GetByUserGidQuery, GetListResponse<GetByUserGidNotificationListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly NoPagination<Notification, GetByUserGidNotificationListItemDto> _noPagination;
        private readonly INotificationReadRepository _notificationReadRepository;

        public GetByUserGidNotificationQueryHandler(IMapper mapper, UserBusinessRules userBusinessRules, NoPagination<Notification, GetByUserGidNotificationListItemDto> noPagination, INotificationReadRepository notificationReadRepository)
        {
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _noPagination = noPagination;
            _notificationReadRepository = notificationReadRepository;
        }

        public async Task<GetListResponse<GetByUserGidNotificationListItemDto>> Handle(GetByUserGidQuery request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserCustomIdShouldExistWhenSelected(request.Gid);

            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: x => x.GidUserFK == request.Gid && x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate,
               includes: new Expression<Func<Notification, object>>[]
                         {
                             x => x.UserFK,
                         });
            }

            IPaginate<Notification> notification = await _notificationReadRepository.GetListAsync(
                               include: x => x.Include(x => x.UserFK),
                               predicate: x => x.GidUserFK == request.Gid && x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate,
                               index: request.PageIndex,
                               size: request.PageSize,
                               cancellationToken: cancellationToken
                                );

            GetListResponse<GetByUserGidNotificationListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidNotificationListItemDto>>(notification);


            return response;
        }



    }
}
