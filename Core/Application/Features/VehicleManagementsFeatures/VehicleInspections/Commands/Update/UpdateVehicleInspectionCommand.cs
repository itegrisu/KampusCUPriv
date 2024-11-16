using Application.Features.VehicleManagementFeatures.VehicleInspections.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Update;

public class UpdateVehicleInspectionCommand : IRequest<UpdatedVehicleInspectionResponse>
{
    public Guid Gid { get; set; }

    public Guid GidVehicleFK { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }



    public class UpdateVehicleInspectionCommandHandler : IRequestHandler<UpdateVehicleInspectionCommand, UpdatedVehicleInspectionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInspectionWriteRepository _vehicleInspectionWriteRepository;
        private readonly IVehicleInspectionReadRepository _vehicleInspectionReadRepository;
        private readonly VehicleInspectionBusinessRules _vehicleInspectionBusinessRules;

        public UpdateVehicleInspectionCommandHandler(IMapper mapper, IVehicleInspectionWriteRepository vehicleInspectionWriteRepository,
                                         VehicleInspectionBusinessRules vehicleInspectionBusinessRules, IVehicleInspectionReadRepository vehicleInspectionReadRepository)
        {
            _mapper = mapper;
            _vehicleInspectionWriteRepository = vehicleInspectionWriteRepository;
            _vehicleInspectionBusinessRules = vehicleInspectionBusinessRules;
            _vehicleInspectionReadRepository = vehicleInspectionReadRepository;
        }

        public async Task<UpdatedVehicleInspectionResponse> Handle(UpdateVehicleInspectionCommand request, CancellationToken cancellationToken)
        {
            X.VehicleInspection? vehicleInspection = await _vehicleInspectionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleInspectionBusinessRules.VehicleInspectionShouldExistWhenSelected(vehicleInspection);
            vehicleInspection = _mapper.Map(request, vehicleInspection);

            _vehicleInspectionWriteRepository.Update(vehicleInspection!);
            await _vehicleInspectionWriteRepository.SaveAsync();
            GetByGidVehicleInspectionResponse obj = _mapper.Map<GetByGidVehicleInspectionResponse>(vehicleInspection);

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