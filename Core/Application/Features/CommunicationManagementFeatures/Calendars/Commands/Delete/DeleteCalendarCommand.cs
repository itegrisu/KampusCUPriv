using Application.Features.CommunicationFeatures.Calendars.Constants;
using Application.Features.CommunicationFeatures.Calendars.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Application.Repositories.CommunicationManagementRepo.CalendarRepo;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Delete;

public class DeleteCalendarCommand : IRequest<DeletedCalendarResponse>
{
	public Guid Gid { get; set; }

    public class DeleteCalendarCommandHandler : IRequestHandler<DeleteCalendarCommand, DeletedCalendarResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICalendarReadRepository _calendarReadRepository;
        private readonly ICalendarWriteRepository _calendarWriteRepository;
        private readonly CalendarBusinessRules _calendarBusinessRules;

        public DeleteCalendarCommandHandler(IMapper mapper, ICalendarReadRepository calendarReadRepository,
                                         CalendarBusinessRules calendarBusinessRules, ICalendarWriteRepository calendarWriteRepository)
        {
            _mapper = mapper;
            _calendarReadRepository = calendarReadRepository;
            _calendarBusinessRules = calendarBusinessRules;
            _calendarWriteRepository = calendarWriteRepository;
        }

        public async Task<DeletedCalendarResponse> Handle(DeleteCalendarCommand request, CancellationToken cancellationToken)
        {
            X.Calendar? calendar = await _calendarReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _calendarBusinessRules.CalendarShouldExistWhenSelected(calendar);
            calendar.DataState = Core.Enum.DataState.Deleted;

            _calendarWriteRepository.Update(calendar);
            await _calendarWriteRepository.SaveAsync();

            return new()
            {
                Title = CalendarsBusinessMessages.ProcessCompleted,
                Message = CalendarsBusinessMessages.SuccessDeletedCalendarMessage,
                IsValid = true
            };
        }
    }
}