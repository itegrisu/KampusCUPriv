using Application.Features.TransportationManagementFeatures.TransportationPassengers.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Delete;

public class DeleteTransportationPassengerCommand : IRequest<DeletedTransportationPassengerResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTransportationPassengerCommandHandler : IRequestHandler<DeleteTransportationPassengerCommand, DeletedTransportationPassengerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
        private readonly ITransportationPassengerWriteRepository _transportationPassengerWriteRepository;
        private readonly TransportationPassengerBusinessRules _transportationPassengerBusinessRules;

        public DeleteTransportationPassengerCommandHandler(IMapper mapper, ITransportationPassengerReadRepository transportationPassengerReadRepository,
                                         TransportationPassengerBusinessRules transportationPassengerBusinessRules, ITransportationPassengerWriteRepository transportationPassengerWriteRepository)
        {
            _mapper = mapper;
            _transportationPassengerReadRepository = transportationPassengerReadRepository;
            _transportationPassengerBusinessRules = transportationPassengerBusinessRules;
            _transportationPassengerWriteRepository = transportationPassengerWriteRepository;
        }

        public async Task<DeletedTransportationPassengerResponse> Handle(DeleteTransportationPassengerCommand request, CancellationToken cancellationToken)
        {
            X.TransportationPassenger? transportationPassenger = await _transportationPassengerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _transportationPassengerBusinessRules.TransportationPassengerShouldExistWhenSelected(transportationPassenger);
            transportationPassenger.DataState = Core.Enum.DataState.Deleted;

            _transportationPassengerWriteRepository.Update(transportationPassenger);
            await _transportationPassengerWriteRepository.SaveAsync();

            return new()
            {
                Title = TransportationPassengersBusinessMessages.ProcessCompleted,
                Message = TransportationPassengersBusinessMessages.SuccessDeletedTransportationPassengerMessage,
                IsValid = true
            };
        }
    }
}