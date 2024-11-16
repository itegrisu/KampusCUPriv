using Application.Features.VehicleManagementFeatures.VehicleInsurances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetByGid
{
    public class GetByGidVehicleInsuranceQuery : IRequest<GetByGidVehicleInsuranceResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleInsuranceQueryHandler : IRequestHandler<GetByGidVehicleInsuranceQuery, GetByGidVehicleInsuranceResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleInsuranceReadRepository _vehicleInsuranceReadRepository;
            private readonly VehicleInsuranceBusinessRules _vehicleInsuranceBusinessRules;

            public GetByGidVehicleInsuranceQueryHandler(IMapper mapper, IVehicleInsuranceReadRepository vehicleInsuranceReadRepository, VehicleInsuranceBusinessRules vehicleInsuranceBusinessRules)
            {
                _mapper = mapper;
                _vehicleInsuranceReadRepository = vehicleInsuranceReadRepository;
                _vehicleInsuranceBusinessRules = vehicleInsuranceBusinessRules;
            }

            public async Task<GetByGidVehicleInsuranceResponse> Handle(GetByGidVehicleInsuranceQuery request, CancellationToken cancellationToken)
            {
                X.VehicleInsurance? vehicleInsurance = await _vehicleInsuranceReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleInsuranceBusinessRules.VehicleInsuranceShouldExistWhenSelected(vehicleInsurance);

                GetByGidVehicleInsuranceResponse response = _mapper.Map<GetByGidVehicleInsuranceResponse>(vehicleInsurance);
                return response;
            }
        }
    }
}