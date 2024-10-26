using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByUserGid
{
    public class GetByUserGidListUserReminderQuery : IRequest<GetListResponse<GetByUserGidListUserReminderListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByUserGidListUserReminderQueryHandler : IRequestHandler<GetByUserGidListUserReminderQuery, GetListResponse<GetByUserGidListUserReminderListItemDto>>
        {
            private readonly IUserReminderReadRepository _userReminderReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.UserReminder, GetByUserGidListUserReminderListItemDto> _noPagination;

            public GetByUserGidListUserReminderQueryHandler(IUserReminderReadRepository userReminderReadRepository, IMapper mapper, NoPagination<X.UserReminder, GetByUserGidListUserReminderListItemDto> noPagination)
            {
                _userReminderReadRepository = userReminderReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListUserReminderListItemDto>> Handle(GetByUserGidListUserReminderQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidUserFK == request.Gid,
                        includes: new Expression<Func<UserReminder, object>>[]
                        {
                       x => x.UserFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.UserReminder> userReminders = await _userReminderReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.UserFK),
                    predicate: x => x.GidUserFK == request.Gid
                );

                GetListResponse<GetByUserGidListUserReminderListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListUserReminderListItemDto>>(userReminders);
                return response;
            }
        }
    }
}
