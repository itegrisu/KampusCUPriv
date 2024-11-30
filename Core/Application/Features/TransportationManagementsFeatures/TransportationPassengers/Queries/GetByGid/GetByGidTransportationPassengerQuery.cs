using Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid
{
    public class GetByGidTransportationPassengerQuery : IRequest<GetByGidTransportationPassengerResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTransportationPassengerQueryHandler : IRequestHandler<GetByGidTransportationPassengerQuery, GetByGidTransportationPassengerResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
            private readonly TransportationPassengerBusinessRules _transportationPassengerBusinessRules;

            public GetByGidTransportationPassengerQueryHandler(IMapper mapper, ITransportationPassengerReadRepository transportationPassengerReadRepository, TransportationPassengerBusinessRules transportationPassengerBusinessRules)
            {
                _mapper = mapper;
                _transportationPassengerReadRepository = transportationPassengerReadRepository;
                _transportationPassengerBusinessRules = transportationPassengerBusinessRules;
            }

            public async Task<GetByGidTransportationPassengerResponse> Handle(GetByGidTransportationPassengerQuery request, CancellationToken cancellationToken)
            {
                X.TransportationPassenger? transportationPassenger = await _transportationPassengerReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationGroupFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _transportationPassengerBusinessRules.TransportationPassengerShouldExistWhenSelected(transportationPassenger);

                GetByGidTransportationPassengerResponse response = _mapper.Map<GetByGidTransportationPassengerResponse>(transportationPassenger);
                return response;
            }
        }
    }
}