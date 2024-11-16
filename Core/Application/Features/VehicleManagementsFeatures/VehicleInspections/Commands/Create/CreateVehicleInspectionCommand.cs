using Application.Features.VehicleManagementFeatures.VehicleInspections.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Create;

public class CreateVehicleInspectionCommand : IRequest<CreatedVehicleInspectionResponse>
{
    public Guid GidVehicleFK { get; set; }

public DateTime StartDate { get; set; }
public DateTime EndDate { get; set; }
public string? DocumentFile { get; set; }
public string? Description { get; set; }



    public class CreateVehicleInspectionCommandHandler : IRequestHandler<CreateVehicleInspectionCommand, CreatedVehicleInspectionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInspectionWriteRepository _vehicleInspectionWriteRepository;
        private readonly IVehicleInspectionReadRepository _vehicleInspectionReadRepository;
        private readonly VehicleInspectionBusinessRules _vehicleInspectionBusinessRules;

        public CreateVehicleInspectionCommandHandler(IMapper mapper, IVehicleInspectionWriteRepository vehicleInspectionWriteRepository,
                                         VehicleInspectionBusinessRules vehicleInspectionBusinessRules, IVehicleInspectionReadRepository vehicleInspectionReadRepository)
        {
            _mapper = mapper;
            _vehicleInspectionWriteRepository = vehicleInspectionWriteRepository;
            _vehicleInspectionBusinessRules = vehicleInspectionBusinessRules;
            _vehicleInspectionReadRepository = vehicleInspectionReadRepository;
        }

        public async Task<CreatedVehicleInspectionResponse> Handle(CreateVehicleInspectionCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleInspectionReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.VehicleInspection vehicleInspection = _mapper.Map<X.VehicleInspection>(request);
            //vehicleInspection.RowNo = maxRowNo + 1;

            await _vehicleInspectionWriteRepository.AddAsync(vehicleInspection);
            await _vehicleInspectionWriteRepository.SaveAsync();

			X.VehicleInspection savedVehicleInspection = await _vehicleInspectionReadRepository.GetAsync(predicate: x => x.Gid == vehicleInspection.Gid, include: x => x.Include(x => x.VehicleAllFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidVehicleInspectionResponse obj = _mapper.Map<GetByGidVehicleInspectionResponse>(savedVehicleInspection);
            return new()
            {           
                Title = VehicleInspectionsBusinessMessages.ProcessCompleted,
                Message = VehicleInspectionsBusinessMessages.SuccessCreatedVehicleInspectionMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}