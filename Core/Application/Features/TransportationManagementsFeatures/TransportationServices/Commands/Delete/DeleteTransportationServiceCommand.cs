using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Delete;

public class DeleteTransportationServiceCommand : IRequest<DeletedTransportationServiceResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTransportationServiceCommandHandler : IRequestHandler<DeleteTransportationServiceCommand, DeletedTransportationServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
        private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
        private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;

        public DeleteTransportationServiceCommandHandler(IMapper mapper, ITransportationServiceReadRepository transportationServiceReadRepository,
                                         TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceWriteRepository transportationServiceWriteRepository)
        {
            _mapper = mapper;
            _transportationServiceReadRepository = transportationServiceReadRepository;
            _transportationServiceBusinessRules = transportationServiceBusinessRules;
            _transportationServiceWriteRepository = transportationServiceWriteRepository;
        }

        public async Task<DeletedTransportationServiceResponse> Handle(DeleteTransportationServiceCommand request, CancellationToken cancellationToken)
        {
            X.TransportationService? transportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);
            transportationService.DataState = Core.Enum.DataState.Deleted;

            _transportationServiceWriteRepository.Update(transportationService);
            await _transportationServiceWriteRepository.SaveAsync();

            return new()
            {
                Title = TransportationServicesBusinessMessages.ProcessCompleted,
                Message = TransportationServicesBusinessMessages.SuccessDeletedTransportationServiceMessage,
                IsValid = true
            };
        }
    }
}