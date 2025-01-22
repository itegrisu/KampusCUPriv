using Application.Features.CommunicationFeatures.Calendars.Constants;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Calendars.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.CommunicationManagementRepo.CalendarRepo;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Create;

public class CreateCalendarCommand : IRequest<CreatedCalendarResponse>
{
    public Guid GidEventFK { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string? Color { get; set; }



    public class CreateCalendarCommandHandler : IRequestHandler<CreateCalendarCommand, CreatedCalendarResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICalendarWriteRepository _calendarWriteRepository;
        private readonly ICalendarReadRepository _calendarReadRepository;
        private readonly CalendarBusinessRules _calendarBusinessRules;

        public CreateCalendarCommandHandler(IMapper mapper, ICalendarWriteRepository calendarWriteRepository,
                                         CalendarBusinessRules calendarBusinessRules, ICalendarReadRepository calendarReadRepository)
        {
            _mapper = mapper;
            _calendarWriteRepository = calendarWriteRepository;
            _calendarBusinessRules = calendarBusinessRules;
            _calendarReadRepository = calendarReadRepository;
        }

        public async Task<CreatedCalendarResponse> Handle(CreateCalendarCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _calendarReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Calendar calendar = _mapper.Map<X.Calendar>(request);
            //calendar.RowNo = maxRowNo + 1;

            await _calendarWriteRepository.AddAsync(calendar);
            await _calendarWriteRepository.SaveAsync();

            X.Calendar savedCalendar = await _calendarReadRepository.GetAsync(predicate: x => x.Gid == calendar.Gid, include: x => x.Include(x => x.EventFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidCalendarResponse obj = _mapper.Map<GetByGidCalendarResponse>(savedCalendar);
            return new()
            {
                Title = CalendarsBusinessMessages.ProcessCompleted,
                Message = CalendarsBusinessMessages.SuccessCreatedCalendarMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}