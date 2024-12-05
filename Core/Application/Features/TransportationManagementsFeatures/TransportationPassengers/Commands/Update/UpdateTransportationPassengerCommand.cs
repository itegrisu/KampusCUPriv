using Application.Features.TransportationManagementFeatures.TransportationPassengers.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Update;

public class UpdateTransportationPassengerCommand : IRequest<UpdatedTransportationPassengerResponse>
{
    public Guid Gid { get; set; }
    public Guid GidTransportationGroupFK { get; set; }
    public string Country { get; set; }
    public string IdentityNo { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EnumGender Gender { get; set; }
    public string? Phone { get; set; }
    public EnumPassengerStatus PassengerStatus { get; set; }
    public string? RefNoTransportationPassenger { get; set; }



    public class UpdateTransportationPassengerCommandHandler : IRequestHandler<UpdateTransportationPassengerCommand, UpdatedTransportationPassengerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationPassengerWriteRepository _transportationPassengerWriteRepository;
        private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
        private readonly TransportationPassengerBusinessRules _transportationPassengerBusinessRules;

        public UpdateTransportationPassengerCommandHandler(IMapper mapper, ITransportationPassengerWriteRepository transportationPassengerWriteRepository,
                                         TransportationPassengerBusinessRules transportationPassengerBusinessRules, ITransportationPassengerReadRepository transportationPassengerReadRepository)
        {
            _mapper = mapper;
            _transportationPassengerWriteRepository = transportationPassengerWriteRepository;
            _transportationPassengerBusinessRules = transportationPassengerBusinessRules;
            _transportationPassengerReadRepository = transportationPassengerReadRepository;
        }

        public async Task<UpdatedTransportationPassengerResponse> Handle(UpdateTransportationPassengerCommand request, CancellationToken cancellationToken)
        {
            X.TransportationPassenger? transportationPassenger = await _transportationPassengerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationGroupFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _transportationPassengerBusinessRules.TransportationPassengerShouldExistWhenSelected(transportationPassenger);
            transportationPassenger = _mapper.Map(request, transportationPassenger);

            _transportationPassengerWriteRepository.Update(transportationPassenger!);
            await _transportationPassengerWriteRepository.SaveAsync();
            GetByGidTransportationPassengerResponse obj = _mapper.Map<GetByGidTransportationPassengerResponse>(transportationPassenger);

            return new()
            {
                Title = TransportationPassengersBusinessMessages.ProcessCompleted,
                Message = TransportationPassengersBusinessMessages.SuccessCreatedTransportationPassengerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}