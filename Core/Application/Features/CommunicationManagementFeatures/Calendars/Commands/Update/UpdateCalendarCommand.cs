using Application.Features.CommunicationFeatures.Calendars.Constants;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Calendars.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Application.Repositories.CommunicationManagementRepo.CalendarRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Update;

public class UpdateCalendarCommand : IRequest<UpdatedCalendarResponse>
{
    public Guid Gid { get; set; }

	public Guid GidEventFK { get; set; }

public string Name { get; set; }
public DateTime Date { get; set; }
public string? Color { get; set; }



    public class UpdateCalendarCommandHandler : IRequestHandler<UpdateCalendarCommand, UpdatedCalendarResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICalendarWriteRepository _calendarWriteRepository;
        private readonly ICalendarReadRepository _calendarReadRepository;
        private readonly CalendarBusinessRules _calendarBusinessRules;

        public UpdateCalendarCommandHandler(IMapper mapper, ICalendarWriteRepository calendarWriteRepository,
                                         CalendarBusinessRules calendarBusinessRules, ICalendarReadRepository calendarReadRepository)
        {
            _mapper = mapper;
            _calendarWriteRepository = calendarWriteRepository;
            _calendarBusinessRules = calendarBusinessRules;
            _calendarReadRepository = calendarReadRepository;
        }

        public async Task<UpdatedCalendarResponse> Handle(UpdateCalendarCommand request, CancellationToken cancellationToken)
        {
            X.Calendar? calendar = await _calendarReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.EventFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _calendarBusinessRules.CalendarShouldExistWhenSelected(calendar);
            calendar = _mapper.Map(request, calendar);

            _calendarWriteRepository.Update(calendar!);
            await _calendarWriteRepository.SaveAsync();
            GetByGidCalendarResponse obj = _mapper.Map<GetByGidCalendarResponse>(calendar);

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