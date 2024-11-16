using Application.Features.VehicleManagementFeatures.VehicleDocuments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Create;

public class CreateVehicleDocumentCommand : IRequest<CreatedVehicleDocumentResponse>
{
    public Guid GidVehicleFK { get; set; }
    public Guid GidDocumentType { get; set; }

    public string DocumentName { get; set; }
    public DateTime DocumentDate { get; set; }
    public DateTime? DocumentLastDate { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }



    public class CreateVehicleDocumentCommandHandler : IRequestHandler<CreateVehicleDocumentCommand, CreatedVehicleDocumentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleDocumentWriteRepository _vehicleDocumentWriteRepository;
        private readonly IVehicleDocumentReadRepository _vehicleDocumentReadRepository;
        private readonly VehicleDocumentBusinessRules _vehicleDocumentBusinessRules;

        public CreateVehicleDocumentCommandHandler(IMapper mapper, IVehicleDocumentWriteRepository vehicleDocumentWriteRepository,
                                         VehicleDocumentBusinessRules vehicleDocumentBusinessRules, IVehicleDocumentReadRepository vehicleDocumentReadRepository)
        {
            _mapper = mapper;
            _vehicleDocumentWriteRepository = vehicleDocumentWriteRepository;
            _vehicleDocumentBusinessRules = vehicleDocumentBusinessRules;
            _vehicleDocumentReadRepository = vehicleDocumentReadRepository;
        }

        public async Task<CreatedVehicleDocumentResponse> Handle(CreateVehicleDocumentCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleDocumentReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleDocument vehicleDocument = _mapper.Map<X.VehicleDocument>(request);
            //vehicleDocument.RowNo = maxRowNo + 1;

            await _vehicleDocumentWriteRepository.AddAsync(vehicleDocument);
            await _vehicleDocumentWriteRepository.SaveAsync();

            X.VehicleDocument savedVehicleDocument = await _vehicleDocumentReadRepository.GetAsync(predicate: x => x.Gid == vehicleDocument.Gid,
                include: x => x.Include(x => x.VehicleAllFK).Include(x => x.DocumentTypeFK)
                );
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidVehicleDocumentResponse obj = _mapper.Map<GetByGidVehicleDocumentResponse>(savedVehicleDocument);
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