using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetList;

public class GetListUserReminderQuery : IRequest<GetListResponse<GetListUserReminderListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserReminderQueryHandler : IRequestHandler<GetListUserReminderQuery, GetListResponse<GetListUserReminderListItemDto>>
    {
        private readonly IUserReminderReadRepository _userReminderReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.UserReminder, GetListUserReminderListItemDto> _noPagination;

        public GetListUserReminderQueryHandler(IUserReminderReadRepository userReminderReadRepository, IMapper mapper, NoPagination<X.UserReminder, GetListUserReminderListItemDto> noPagination)
        {
            _userReminderReadRepository = userReminderReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListUserReminderListItemDto>> Handle(GetListUserReminderQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<UserReminder, object>>[]
                    {
                       x => x.UserFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.UserReminder> userReminders = await _userReminderReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK)
            );

            GetListResponse<GetListUserReminderListItemDto> response = _mapper.Map<GetListResponse<GetListUserReminderListItemDto>>(userReminders);
            return response;
        }
    }
}