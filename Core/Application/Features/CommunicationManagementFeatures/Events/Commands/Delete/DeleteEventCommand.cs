using Application.Features.CommunicationFeatures.Events.Constants;
using Application.Features.CommunicationFeatures.Events.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Events.Commands.Delete;

public class DeleteEventCommand : IRequest<DeletedEventResponse>
{
	public Guid Gid { get; set; }

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, DeletedEventResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEventReadRepository _eventReadRepository;
        private readonly IEventWriteRepository _eventWriteRepository;
        private readonly EventBusinessRules _eventBusinessRules;

        public DeleteEventCommandHandler(IMapper mapper, IEventReadRepository eventReadRepository,
                                         EventBusinessRules eventBusinessRules, IEventWriteRepository eventWriteRepository)
        {
            _mapper = mapper;
            _eventReadRepository = eventReadRepository;
            _eventBusinessRules = eventBusinessRules;
            _eventWriteRepository = eventWriteRepository;
        }

        public async Task<DeletedEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            X.Event? event1 = await _eventReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _eventBusinessRules.EventShouldExistWhenSelected(event1);
            event1.EventStatus = EnumEventStatus.Canceled;

            _eventWriteRepository.Update(event1);
            await _eventWriteRepository.SaveAsync();

            return new()
            {
                Title = EventsBusinessMessages.ProcessCompleted,
                Message = EventsBusinessMessages.SuccessDeletedEventMessage,
                IsValid = true
            };
        }
    }
}