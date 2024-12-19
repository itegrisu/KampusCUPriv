using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.NotificationManagementRepos;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.NotificationManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.NotificationManagementFeatures.Notifications.Queries.GetUnreadByUserGid
{
    public class GetUnreadByUserGidQuery : IRequest<GetListResponse<GetUnreadByUserGidListItemDto>>
    {
        public Guid UserGid { get; set; }

        public class GetUnreadByUserGidQueryHandler : IRequestHandler<GetUnreadByUserGidQuery, GetListResponse<GetUnreadByUserGidListItemDto>>
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly NoPagination<Notification, GetUnreadByUserGidListItemDto> _noPagination;
            private readonly INotificationReadRepository _notificationReadRepository;
            public GetUnreadByUserGidQueryHandler(IMediator mediator, IMapper mapper, UserBusinessRules userBusinessRules, INotificationReadRepository notificationReadRepository, NoPagination<Notification, GetUnreadByUserGidListItemDto> noPagination)
            {
                _mediator = mediator;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
                _notificationReadRepository = notificationReadRepository;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetUnreadByUserGidListItemDto>> Handle(GetUnreadByUserGidQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserCustomIdShouldExistWhenSelected(request.UserGid);

                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: x => x.GidUserFK == request.UserGid && x.ReadingDate == null,
                    includes: new Expression<Func<Notification, object>>[]
                    {
                        x => x.UserFK,
                    });
            }
        }
    }
}
