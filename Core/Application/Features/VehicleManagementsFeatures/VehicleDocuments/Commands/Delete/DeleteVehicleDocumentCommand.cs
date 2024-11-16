using Application.Features.VehicleManagementFeatures.VehicleDocuments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Delete;

public class DeleteVehicleDocumentCommand : IRequest<DeletedVehicleDocumentResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleDocumentCommandHandler : IRequestHandler<DeleteVehicleDocumentCommand, DeletedVehicleDocumentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleDocumentReadRepository _vehicleDocumentReadRepository;
        private readonly IVehicleDocumentWriteRepository _vehicleDocumentWriteRepository;
        private readonly VehicleDocumentBusinessRules _vehicleDocumentBusinessRules;

        public DeleteVehicleDocumentCommandHandler(IMapper mapper, IVehicleDocumentReadRepository vehicleDocumentReadRepository,
                                         VehicleDocumentBusinessRules vehicleDocumentBusinessRules, IVehicleDocumentWriteRepository vehicleDocumentWriteRepository)
        {
            _mapper = mapper;
            _vehicleDocumentReadRepository = vehicleDocumentReadRepository;
            _vehicleDocumentBusinessRules = vehicleDocumentBusinessRules;
            _vehicleDocumentWriteRepository = vehicleDocumentWriteRepository;
        }

        public async Task<DeletedVehicleDocumentResponse> Handle(DeleteVehicleDocumentCommand request, CancellationToken cancellationToken)
        {
            X.VehicleDocument? vehicleDocument = await _vehicleDocumentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleDocumentBusinessRules.VehicleDocumentShouldExistWhenSelected(vehicleDocument);
            vehicleDocument.DataState = Core.Enum.DataState.Deleted;

            _vehicleDocumentWriteRepository.Update(vehicleDocument);
            await _vehicleDocumentWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleDocumentsBusinessMessages.ProcessCompleted,
                Message = VehicleDocumentsBusinessMessages.SuccessDeletedVehicleDocumentMessage,
                IsValid = true
            };
        }
    }
}