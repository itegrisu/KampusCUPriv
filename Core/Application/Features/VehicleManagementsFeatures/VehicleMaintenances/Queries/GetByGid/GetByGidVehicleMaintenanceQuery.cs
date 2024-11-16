using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleMaintenanceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid
{
    public class GetByGidVehicleMaintenanceQuery : IRequest<GetByGidVehicleMaintenanceResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleMaintenanceQueryHandler : IRequestHandler<GetByGidVehicleMaintenanceQuery, GetByGidVehicleMaintenanceResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleMaintenanceReadRepository _vehicleMaintenanceReadRepository;
            private readonly VehicleMaintenanceBusinessRules _vehicleMaintenanceBusinessRules;

            public GetByGidVehicleMaintenanceQueryHandler(IMapper mapper, IVehicleMaintenanceReadRepository vehicleMaintenanceReadRepository, VehicleMaintenanceBusinessRules vehicleMaintenanceBusinessRules)
            {
                _mapper = mapper;
                _vehicleMaintenanceReadRepository = vehicleMaintenanceReadRepository;
                _vehicleMaintenanceBusinessRules = vehicleMaintenanceBusinessRules;
            }

            public async Task<GetByGidVehicleMaintenanceResponse> Handle(GetByGidVehicleMaintenanceQuery request, CancellationToken cancellationToken)
            {
                X.VehicleMaintenance? vehicleMaintenance = await _vehicleMaintenanceReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleMaintenanceBusinessRules.VehicleMaintenanceShouldExistWhenSelected(vehicleMaintenance);

                GetByGidVehicleMaintenanceResponse response = _mapper.Map<GetByGidVehicleMaintenanceResponse>(vehicleMaintenance);
                return response;
            }
        }
    }
}