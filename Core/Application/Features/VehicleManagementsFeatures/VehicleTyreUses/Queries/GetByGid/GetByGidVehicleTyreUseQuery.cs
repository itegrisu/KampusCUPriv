using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid
{
    public class GetByGidVehicleTyreUseQuery : IRequest<GetByGidVehicleTyreUseResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleTyreUseQueryHandler : IRequestHandler<GetByGidVehicleTyreUseQuery, GetByGidVehicleTyreUseResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleTyreUseReadRepository _vehicleTyreUseReadRepository;
            private readonly VehicleTyreUseBusinessRules _vehicleTyreUseBusinessRules;

            public GetByGidVehicleTyreUseQueryHandler(IMapper mapper, IVehicleTyreUseReadRepository vehicleTyreUseReadRepository, VehicleTyreUseBusinessRules vehicleTyreUseBusinessRules)
            {
                _mapper = mapper;
                _vehicleTyreUseReadRepository = vehicleTyreUseReadRepository;
                _vehicleTyreUseBusinessRules = vehicleTyreUseBusinessRules;
            }

            public async Task<GetByGidVehicleTyreUseResponse> Handle(GetByGidVehicleTyreUseQuery request, CancellationToken cancellationToken)
            {
                X.VehicleTyreUse? vehicleTyreUse = await _vehicleTyreUseReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK).Include(x => x.TyreFK).Include(x => x.TyreFK).ThenInclude(x => x.TyreTypeFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleTyreUseBusinessRules.VehicleTyreUseShouldExistWhenSelected(vehicleTyreUse);

                GetByGidVehicleTyreUseResponse response = _mapper.Map<GetByGidVehicleTyreUseResponse>(vehicleTyreUse);
                return response;
            }
        }
    }
}