using Application.Features.VehicleManagementFeatures.VehicleDocuments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Update;

public class UpdateVehicleDocumentCommand : IRequest<UpdatedVehicleDocumentResponse>
{
    public Guid Gid { get; set; }

    public Guid GidVehicleFK { get; set; }
    public Guid GidDocumentType { get; set; }
    public string DocumentName { get; set; }
    public DateTime DocumentDate { get; set; }
    public DateTime? DocumentLastDate { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }



    public class UpdateVehicleDocumentCommandHandler : IRequestHandler<UpdateVehicleDocumentCommand, UpdatedVehicleDocumentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleDocumentWriteRepository _vehicleDocumentWriteRepository;
        private readonly IVehicleDocumentReadRepository _vehicleDocumentReadRepository;
        private readonly VehicleDocumentBusinessRules _vehicleDocumentBusinessRules;

        public UpdateVehicleDocumentCommandHandler(IMapper mapper, IVehicleDocumentWriteRepository vehicleDocumentWriteRepository,
                                         VehicleDocumentBusinessRules vehicleDocumentBusinessRules, IVehicleDocumentReadRepository vehicleDocumentReadRepository)
        {
            _mapper = mapper;
            _vehicleDocumentWriteRepository = vehicleDocumentWriteRepository;
            _vehicleDocumentBusinessRules = vehicleDocumentBusinessRules;
            _vehicleDocumentReadRepository = vehicleDocumentReadRepository;
        }

        public async Task<UpdatedVehicleDocumentResponse> Handle(UpdateVehicleDocumentCommand request, CancellationToken cancellationToken)
        {
            X.VehicleDocument? vehicleDocument = await _vehicleDocumentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK).Include(x => x.DocumentTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleDocumentBusinessRules.VehicleDocumentShouldExistWhenSelected(vehicleDocument);
            vehicleDocument = _mapper.Map(request, vehicleDocument);

            _vehicleDocumentWriteRepository.Update(vehicleDocument!);
            await _vehicleDocumentWriteRepository.SaveAsync();
            GetByGidVehicleDocumentResponse obj = _mapper.Map<GetByGidVehicleDocumentResponse>(vehicleDocument);

            return new()
            {
                Title = VehicleDocumentsBusinessMessages.ProcessCompleted,
                Message = VehicleDocumentsBusinessMessages.SuccessCreatedVehicleDocumentMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}