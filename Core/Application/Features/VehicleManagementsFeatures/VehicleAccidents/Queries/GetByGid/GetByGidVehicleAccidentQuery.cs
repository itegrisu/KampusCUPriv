using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetByGid
{
    public class GetByGidVehicleAccidentQuery : IRequest<GetByGidVehicleAccidentResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleAccidentQueryHandler : IRequestHandler<GetByGidVehicleAccidentQuery, GetByGidVehicleAccidentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleAccidentReadRepository _vehicleAccidentReadRepository;
            private readonly VehicleAccidentBusinessRules _vehicleAccidentBusinessRules;

            public GetByGidVehicleAccidentQueryHandler(IMapper mapper, IVehicleAccidentReadRepository vehicleAccidentReadRepository, VehicleAccidentBusinessRules vehicleAccidentBusinessRules)
            {
                _mapper = mapper;
                _vehicleAccidentReadRepository = vehicleAccidentReadRepository;
                _vehicleAccidentBusinessRules = vehicleAccidentBusinessRules;
            }

            public async Task<GetByGidVehicleAccidentResponse> Handle(GetByGidVehicleAccidentQuery request, CancellationToken cancellationToken)
            {
                X.VehicleAccident? vehicleAccident = await _vehicleAccidentReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
                   
                await _vehicleAccidentBusinessRules.VehicleAccidentShouldExistWhenSelected(vehicleAccident);

                GetByGidVehicleAccidentResponse response = _mapper.Map<GetByGidVehicleAccidentResponse>(vehicleAccident);
                return response;
            }
        }
    }
}