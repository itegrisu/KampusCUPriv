using Application.Features.VehicleManagementFeatures.VehicleInspections.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid
{
    public class GetByGidVehicleInspectionQuery : IRequest<GetByGidVehicleInspectionResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleInspectionQueryHandler : IRequestHandler<GetByGidVehicleInspectionQuery, GetByGidVehicleInspectionResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleInspectionReadRepository _vehicleInspectionReadRepository;
            private readonly VehicleInspectionBusinessRules _vehicleInspectionBusinessRules;

            public GetByGidVehicleInspectionQueryHandler(IMapper mapper, IVehicleInspectionReadRepository vehicleInspectionReadRepository, VehicleInspectionBusinessRules vehicleInspectionBusinessRules)
            {
                _mapper = mapper;
                _vehicleInspectionReadRepository = vehicleInspectionReadRepository;
                _vehicleInspectionBusinessRules = vehicleInspectionBusinessRules;
            }

            public async Task<GetByGidVehicleInspectionResponse> Handle(GetByGidVehicleInspectionQuery request, CancellationToken cancellationToken)
            {
                X.VehicleInspection? vehicleInspection = await _vehicleInspectionReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleInspectionBusinessRules.VehicleInspectionShouldExistWhenSelected(vehicleInspection);

                GetByGidVehicleInspectionResponse response = _mapper.Map<GetByGidVehicleInspectionResponse>(vehicleInspection);
                return response;
            }
        }
    }
}