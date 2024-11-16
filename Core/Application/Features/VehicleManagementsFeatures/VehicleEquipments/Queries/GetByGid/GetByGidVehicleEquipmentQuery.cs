using Application.Features.VehicleManagementFeatures.VehicleEquipments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid
{
    public class GetByGidVehicleEquipmentQuery : IRequest<GetByGidVehicleEquipmentResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleEquipmentQueryHandler : IRequestHandler<GetByGidVehicleEquipmentQuery, GetByGidVehicleEquipmentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleEquipmentReadRepository _vehicleEquipmentReadRepository;
            private readonly VehicleEquipmentBusinessRules _vehicleEquipmentBusinessRules;

            public GetByGidVehicleEquipmentQueryHandler(IMapper mapper, IVehicleEquipmentReadRepository vehicleEquipmentReadRepository, VehicleEquipmentBusinessRules vehicleEquipmentBusinessRules)
            {
                _mapper = mapper;
                _vehicleEquipmentReadRepository = vehicleEquipmentReadRepository;
                _vehicleEquipmentBusinessRules = vehicleEquipmentBusinessRules;
            }

            public async Task<GetByGidVehicleEquipmentResponse> Handle(GetByGidVehicleEquipmentQuery request, CancellationToken cancellationToken)
            {
                X.VehicleEquipment? vehicleEquipment = await _vehicleEquipmentReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleEquipmentBusinessRules.VehicleEquipmentShouldExistWhenSelected(vehicleEquipment);

                GetByGidVehicleEquipmentResponse response = _mapper.Map<GetByGidVehicleEquipmentResponse>(vehicleEquipment);
                return response;
            }
        }
    }
}