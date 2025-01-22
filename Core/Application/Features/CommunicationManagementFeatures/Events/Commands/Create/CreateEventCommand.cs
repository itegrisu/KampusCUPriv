using Application.Features.CommunicationFeatures.Events.Constants;
using Application.Features.CommunicationFeatures.Events.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Events.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Events.Commands.Create;

public class CreateEventCommand : IRequest<CreatedEventResponse>
{
    public Guid GidClubFK { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public EnumEventStatus EventStatus { get; set; }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CreatedEventResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEventWriteRepository _eventWriteRepository;
        private readonly IEventReadRepository _eventReadRepository;
        private readonly EventBusinessRules _eventBusinessRules;

        public CreateEventCommandHandler(IMapper mapper, IEventWriteRepository eventWriteRepository,
                                         EventBusinessRules eventBusinessRules, IEventReadRepository eventReadRepository)
        {
            _mapper = mapper;
            _eventWriteRepository = eventWriteRepository;
            _eventBusinessRules = eventBusinessRules;
            _eventReadRepository = eventReadRepository;
        }

        public async Task<CreatedEventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _eventReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Event event1 = _mapper.Map<X.Event>(request);
            //event.RowNo = maxRowNo + 1;

            await _eventWriteRepository.AddAsync(event1);
            await _eventWriteRepository.SaveAsync();

            X.Event savedEvent = await _eventReadRepository.GetAsync(predicate: x => x.Gid == event1.Gid, include: x => x.Include(x => x.ClubFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidEventResponse obj = _mapper.Map<GetByGidEventResponse>(savedEvent);
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