using Application.Features.VehicleManagementFeatures.VehicleAlls.Constants;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Create;

public class CreateVehicleAllCommand : IRequest<CreatedVehicleAllResponse>
{
    public Guid GidVehicleBrand { get; set; }
    public string PlateNumber { get; set; }
    public EnumVehicleType VehicleType { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public string? EngineNo { get; set; }
    public string? ChassisNumber { get; set; }
    public int PassengerCount { get; set; }
    public EnumFuelType FuelType { get; set; }
    public string? Description { get; set; }

    public class CreateVehicleAllCommandHandler : IRequestHandler<CreateVehicleAllCommand, CreatedVehicleAllResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleAllWriteRepository _vehicleAllWriteRepository;
        private readonly IVehicleAllReadRepository _vehicleAllReadRepository;
        private readonly VehicleAllBusinessRules _vehicleAllBusinessRules;

        public CreateVehicleAllCommandHandler(IMapper mapper, IVehicleAllWriteRepository vehicleAllWriteRepository,
                                         VehicleAllBusinessRules vehicleAllBusinessRules, IVehicleAllReadRepository vehicleAllReadRepository)
        {
            _mapper = mapper;
            _vehicleAllWriteRepository = vehicleAllWriteRepository;
            _vehicleAllBusinessRules = vehicleAllBusinessRules;
            _vehicleAllReadRepository = vehicleAllReadRepository;
        }

        public async Task<CreatedVehicleAllResponse> Handle(CreateVehicleAllCommand request, CancellationToken cancellationToken)
        {
            await _vehicleAllBusinessRules.PlateNumberAllreadyExist(request.PlateNumber);
            //int maxRowNo = await _vehicleAllReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleAll vehicleAll = _mapper.Map<X.VehicleAll>(request);
            //vehicleAll.RowNo = maxRowNo + 1;

            await _vehicleAllWriteRepository.AddAsync(vehicleAll);
            await _vehicleAllWriteRepository.SaveAsync();

            X.VehicleAll savedVehicleAll = await _vehicleAllReadRepository.GetAsync(predicate: x => x.Gid == vehicleAll.Gid, include: x => x.Include( x=> x.OtoBrandFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidVehicleAllResponse obj = _mapper.Map<GetByGidVehicleAllResponse>(savedVehicleAll);
            return new()
            {
                Title = VehicleAllsBusinessMessages.ProcessCompleted,
                Message = VehicleAllsBusinessMessages.SuccessCreatedVehicleAllMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}