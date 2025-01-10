using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.CommunicationManagementRepo.CalendarRepo;

namespace Application.Features.CommunicationFeatures.Calendars.Queries.GetList;

public class GetListCalendarQuery : IRequest<GetListResponse<GetListCalendarListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCalendarQueryHandler : IRequestHandler<GetListCalendarQuery, GetListResponse<GetListCalendarListItemDto>>
    {
        private readonly ICalendarReadRepository _calendarReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Calendar, GetListCalendarListItemDto> _noPagination;

        public GetListCalendarQueryHandler(ICalendarReadRepository calendarReadRepository, IMapper mapper, NoPagination<X.Calendar, GetListCalendarListItemDto> noPagination)
        {
            _calendarReadRepository = calendarReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListCalendarListItemDto>> Handle(GetListCalendarQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<Calendar, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.CalendarMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.Calendar> calendars = await _calendarReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCalendarListItemDto> response = _mapper.Map<GetListResponse<GetListCalendarListItemDto>>(calendars);
            return response;
        }
    }
}