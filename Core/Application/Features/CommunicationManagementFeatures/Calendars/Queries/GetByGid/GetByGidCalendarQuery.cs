using AutoMapper;
using MediatR;
using X = Domain.Entities.CommunicationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.CommunicationFeatures.Calendars.Rules;
using Application.Repositories.CommunicationManagementRepo.CalendarRepo;

namespace Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid
{
    public class GetByGidCalendarQuery : IRequest<GetByGidCalendarResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidCalendarQueryHandler : IRequestHandler<GetByGidCalendarQuery, GetByGidCalendarResponse>
        {
            private readonly IMapper _mapper;
            private readonly ICalendarReadRepository _calendarReadRepository;
            private readonly CalendarBusinessRules _calendarBusinessRules;

            public GetByGidCalendarQueryHandler(IMapper mapper, ICalendarReadRepository calendarReadRepository, CalendarBusinessRules calendarBusinessRules)
            {
                _mapper = mapper;
                _calendarReadRepository = calendarReadRepository;
                _calendarBusinessRules = calendarBusinessRules;
            }

            public async Task<GetByGidCalendarResponse> Handle(GetByGidCalendarQuery request, CancellationToken cancellationToken)
            {
                X.Calendar? calendar = await _calendarReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.EventFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _calendarBusinessRules.CalendarShouldExistWhenSelected(calendar);

                GetByGidCalendarResponse response = _mapper.Map<GetByGidCalendarResponse>(calendar);
                return response;
            }
        }
    }
}