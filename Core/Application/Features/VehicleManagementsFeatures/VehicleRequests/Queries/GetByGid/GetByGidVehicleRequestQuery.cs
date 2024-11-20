using Application.Features.VehicleManagementFeatures.VehicleRequests.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetByGid
{
    public class GetByGidVehicleRequestQuery : IRequest<GetByGidVehicleRequestResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleRequestQueryHandler : IRequestHandler<GetByGidVehicleRequestQuery, GetByGidVehicleRequestResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
            private readonly VehicleRequestBusinessRules _vehicleRequestBusinessRules;

            public GetByGidVehicleRequestQueryHandler(IMapper mapper, IVehicleRequestReadRepository vehicleRequestReadRepository, VehicleRequestBusinessRules vehicleRequestBusinessRules)
            {
                _mapper = mapper;
                _vehicleRequestReadRepository = vehicleRequestReadRepository;
                _vehicleRequestBusinessRules = vehicleRequestBusinessRules;
            }

            public async Task<GetByGidVehicleRequestResponse> Handle(GetByGidVehicleRequestQuery request, CancellationToken cancellationToken)
            {
                X.VehicleRequest? vehicleRequest = await _vehicleRequestReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK).Include(x => x.RequestUserFK).Include(x => x.ApprovedUserFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleRequestBusinessRules.VehicleRequestShouldExistWhenSelected(vehicleRequest);

                GetByGidVehicleRequestResponse response = _mapper.Map<GetByGidVehicleRequestResponse>(vehicleRequest);
                return response;
            }
        }
    }
}