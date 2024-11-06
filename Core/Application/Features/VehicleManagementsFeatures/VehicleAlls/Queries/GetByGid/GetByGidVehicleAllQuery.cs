using Application.Features.VehicleManagementFeatures.VehicleAlls.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid
{
    public class GetByGidVehicleAllQuery : IRequest<GetByGidVehicleAllResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleAllQueryHandler : IRequestHandler<GetByGidVehicleAllQuery, GetByGidVehicleAllResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleAllReadRepository _vehicleAllReadRepository;
            private readonly VehicleAllBusinessRules _vehicleAllBusinessRules;

            public GetByGidVehicleAllQueryHandler(IMapper mapper, IVehicleAllReadRepository vehicleAllReadRepository, VehicleAllBusinessRules vehicleAllBusinessRules)
            {
                _mapper = mapper;
                _vehicleAllReadRepository = vehicleAllReadRepository;
                _vehicleAllBusinessRules = vehicleAllBusinessRules;
            }

            public async Task<GetByGidVehicleAllResponse> Handle(GetByGidVehicleAllQuery request, CancellationToken cancellationToken)
            {
                X.VehicleAll? vehicleAll = await _vehicleAllReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OtoBrandFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleAllBusinessRules.VehicleAllShouldExistWhenSelected(vehicleAll);

                GetByGidVehicleAllResponse response = _mapper.Map<GetByGidVehicleAllResponse>(vehicleAll);
                return response;
            }
        }
    }
}