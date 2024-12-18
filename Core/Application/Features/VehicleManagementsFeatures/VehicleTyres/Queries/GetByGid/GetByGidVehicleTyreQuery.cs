using Application.Features.VehicleManagementFeatures.Tyres.Rules;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid
{
    public class GetByGidVehicleTyreQuery : IRequest<GetByGidVehicleTyreResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTyreQueryHandler : IRequestHandler<GetByGidVehicleTyreQuery, GetByGidVehicleTyreResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleTyreReadRepository _tyreReadRepository;
            private readonly VehicleTyreBusinessRules _tyreBusinessRules;

            public GetByGidTyreQueryHandler(IMapper mapper, IVehicleTyreReadRepository tyreReadRepository, VehicleTyreBusinessRules tyreBusinessRules)
            {
                _mapper = mapper;
                _tyreReadRepository = tyreReadRepository;
                _tyreBusinessRules = tyreBusinessRules;
            }

            public async Task<GetByGidVehicleTyreResponse> Handle(GetByGidVehicleTyreQuery request, CancellationToken cancellationToken)
            {
                X.VehicleTyre? tyre = await _tyreReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TyreTypeFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _tyreBusinessRules.TyreShouldExistWhenSelected(tyre);

                GetByGidVehicleTyreResponse response = _mapper.Map<GetByGidVehicleTyreResponse>(tyre);
                return response;
            }
        }
    }
}