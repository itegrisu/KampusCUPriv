using AutoMapper;
using MediatR;
using X = Domain.Entities.CommunicationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.CommunicationFeatures.Events.Rules;
using Application.Repositories.CommunicationManagementRepo.EventRepo;

namespace Application.Features.CommunicationFeatures.Events.Queries.GetByGid
{
    public class GetByGidEventQuery : IRequest<GetByGidEventResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidEventQueryHandler : IRequestHandler<GetByGidEventQuery, GetByGidEventResponse>
        {
            private readonly IMapper _mapper;
            private readonly IEventReadRepository _eventReadRepository;
            private readonly EventBusinessRules _eventBusinessRules;

            public GetByGidEventQueryHandler(IMapper mapper, IEventReadRepository eventReadRepository, EventBusinessRules eventBusinessRules)
            {
                _mapper = mapper;
                _eventReadRepository = eventReadRepository;
                _eventBusinessRules = eventBusinessRules;
            }

            public async Task<GetByGidEventResponse> Handle(GetByGidEventQuery request, CancellationToken cancellationToken)
            {
                X.Event? event1 = await _eventReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ClubFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _eventBusinessRules.EventShouldExistWhenSelected(event1);

                GetByGidEventResponse response = _mapper.Map<GetByGidEventResponse>(event1);
                return response;
            }
        }
    }
}