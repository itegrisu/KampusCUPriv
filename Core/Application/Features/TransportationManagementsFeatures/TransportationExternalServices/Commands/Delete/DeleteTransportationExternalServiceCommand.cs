using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Rules;
using Application.Repositories.TransportationRepos.TransportationExternalServiceRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Delete;

public class DeleteTransportationExternalServiceCommand : IRequest<DeletedTransportationExternalServiceResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTransportationExternalServiceCommandHandler : IRequestHandler<DeleteTransportationExternalServiceCommand, DeletedTransportationExternalServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationExternalServiceReadRepository _transportationExternalServiceReadRepository;
        private readonly ITransportationExternalServiceWriteRepository _transportationExternalServiceWriteRepository;
        private readonly TransportationExternalServiceBusinessRules _transportationExternalServiceBusinessRules;

        public DeleteTransportationExternalServiceCommandHandler(IMapper mapper, ITransportationExternalServiceReadRepository transportationExternalServiceReadRepository,
                                         TransportationExternalServiceBusinessRules transportationExternalServiceBusinessRules, ITransportationExternalServiceWriteRepository transportationExternalServiceWriteRepository)
        {
            _mapper = mapper;
            _transportationExternalServiceReadRepository = transportationExternalServiceReadRepository;
            _transportationExternalServiceBusinessRules = transportationExternalServiceBusinessRules;
            _transportationExternalServiceWriteRepository = transportationExternalServiceWriteRepository;
        }

        public async Task<DeletedTransportationExternalServiceResponse> Handle(DeleteTransportationExternalServiceCommand request, CancellationToken cancellationToken)
        {
            X.TransportationExternalService? transportationExternalService = await _transportationExternalServiceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _transportationExternalServiceBusinessRules.TransportationExternalServiceShouldExistWhenSelected(transportationExternalService);
            transportationExternalService.DataState = Core.Enum.DataState.Deleted;

            _transportationExternalServiceWriteRepository.Update(transportationExternalService);
            await _transportationExternalServiceWriteRepository.SaveAsync();

            return new()
            {
                Title = TransportationExternalServicesBusinessMessages.ProcessCompleted,
                Message = TransportationExternalServicesBusinessMessages.SuccessDeletedTransportationExternalServiceMessage,
                IsValid = true
            };
        }
    }
}