using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Update;

public class UpdateVehicleTyreUseCommand : IRequest<UpdatedVehicleTyreUseResponse>
{
    public Guid Gid { get; set; }

	public Guid GidVehicleFK { get; set; }
public Guid GidTyreFK { get; set; }

public DateTime InstallationDate { get; set; }
public DateTime? TyreRemovalDate { get; set; }



    public class UpdateVehicleTyreUseCommandHandler : IRequestHandler<UpdateVehicleTyreUseCommand, UpdatedVehicleTyreUseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTyreUseWriteRepository _vehicleTyreUseWriteRepository;
        private readonly IVehicleTyreUseReadRepository _vehicleTyreUseReadRepository;
        private readonly VehicleTyreUseBusinessRules _vehicleTyreUseBusinessRules;

        public UpdateVehicleTyreUseCommandHandler(IMapper mapper, IVehicleTyreUseWriteRepository vehicleTyreUseWriteRepository,
                                         VehicleTyreUseBusinessRules vehicleTyreUseBusinessRules, IVehicleTyreUseReadRepository vehicleTyreUseReadRepository)
        {
            _mapper = mapper;
            _vehicleTyreUseWriteRepository = vehicleTyreUseWriteRepository;
            _vehicleTyreUseBusinessRules = vehicleTyreUseBusinessRules;
            _vehicleTyreUseReadRepository = vehicleTyreUseReadRepository;
        }

        public async Task<UpdatedVehicleTyreUseResponse> Handle(UpdateVehicleTyreUseCommand request, CancellationToken cancellationToken)
        {
            X.VehicleTyreUse? vehicleTyreUse = await _vehicleTyreUseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK).Include(x => x.TyreFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleTyreUseBusinessRules.VehicleTyreUseShouldExistWhenSelected(vehicleTyreUse);
            vehicleTyreUse = _mapper.Map(request, vehicleTyreUse);

            _vehicleTyreUseWriteRepository.Update(vehicleTyreUse!);
            await _vehicleTyreUseWriteRepository.SaveAsync();
            GetByGidVehicleTyreUseResponse obj = _mapper.Map<GetByGidVehicleTyreUseResponse>(vehicleTyreUse);

            return new()
            {
                Title = VehicleTyreUsesBusinessMessages.ProcessCompleted,
                Message = VehicleTyreUsesBusinessMessages.SuccessCreatedVehicleTyreUseMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}