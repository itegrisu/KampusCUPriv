using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Create;

public class CreateVehicleTyreUseCommand : IRequest<CreatedVehicleTyreUseResponse>
{
    public Guid GidVehicleFK { get; set; }
public Guid GidTyreFK { get; set; }

public DateTime InstallationDate { get; set; }
public DateTime? TyreRemovalDate { get; set; }



    public class CreateVehicleTyreUseCommandHandler : IRequestHandler<CreateVehicleTyreUseCommand, CreatedVehicleTyreUseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTyreUseWriteRepository _vehicleTyreUseWriteRepository;
        private readonly IVehicleTyreUseReadRepository _vehicleTyreUseReadRepository;
        private readonly VehicleTyreUseBusinessRules _vehicleTyreUseBusinessRules;

        public CreateVehicleTyreUseCommandHandler(IMapper mapper, IVehicleTyreUseWriteRepository vehicleTyreUseWriteRepository,
                                         VehicleTyreUseBusinessRules vehicleTyreUseBusinessRules, IVehicleTyreUseReadRepository vehicleTyreUseReadRepository)
        {
            _mapper = mapper;
            _vehicleTyreUseWriteRepository = vehicleTyreUseWriteRepository;
            _vehicleTyreUseBusinessRules = vehicleTyreUseBusinessRules;
            _vehicleTyreUseReadRepository = vehicleTyreUseReadRepository;
        }

        public async Task<CreatedVehicleTyreUseResponse> Handle(CreateVehicleTyreUseCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleTyreUseReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.VehicleTyreUse vehicleTyreUse = _mapper.Map<X.VehicleTyreUse>(request);
            //vehicleTyreUse.RowNo = maxRowNo + 1;

            await _vehicleTyreUseWriteRepository.AddAsync(vehicleTyreUse);
            await _vehicleTyreUseWriteRepository.SaveAsync();

			X.VehicleTyreUse savedVehicleTyreUse = await _vehicleTyreUseReadRepository.GetAsync(predicate: x => x.Gid == vehicleTyreUse.Gid,
                include: x => x.Include(x => x.VehicleAllFK).Include(x => x.TyreFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidVehicleTyreUseResponse obj = _mapper.Map<GetByGidVehicleTyreUseResponse>(savedVehicleTyreUse);
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