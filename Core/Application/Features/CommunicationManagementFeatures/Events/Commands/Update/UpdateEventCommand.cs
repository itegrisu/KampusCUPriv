using Application.Features.CommunicationFeatures.Events.Constants;
using Application.Features.CommunicationFeatures.Events.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Events.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Domain.Enums;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CommunicationFeatures.Events.Commands.Update;

public class UpdateEventCommand : IRequest<UpdatedEventResponse>
{
    public Guid Gid { get; set; }
    public Guid GidClubFK { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public EnumEventStatus EventStatus { get; set; }



    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, UpdatedEventResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEventWriteRepository _eventWriteRepository;
        private readonly IEventReadRepository _eventReadRepository;
        private readonly EventBusinessRules _eventBusinessRules;

        public UpdateEventCommandHandler(IMapper mapper, IEventWriteRepository eventWriteRepository,
                                         EventBusinessRules eventBusinessRules, IEventReadRepository eventReadRepository)
        {
            _mapper = mapper;
            _eventWriteRepository = eventWriteRepository;
            _eventBusinessRules = eventBusinessRules;
            _eventReadRepository = eventReadRepository;
        }

        public async Task<UpdatedEventResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            X.Event? event1 = await _eventReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ClubFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _eventBusinessRules.EventShouldExistWhenSelected(event1);
            event1 = _mapper.Map(request, event1);

            _eventWriteRepository.Update(event1);
            await _eventWriteRepository.SaveAsync();
            GetByGidEventResponse obj = _mapper.Map<GetByGidEventResponse>(event1);

            return new()
            {
                Title = EventsBusinessMessages.ProcessCompleted,
                Message = EventsBusinessMessages.SuccessCreatedEventMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}