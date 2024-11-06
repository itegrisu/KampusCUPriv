using Application.Features.VehicleManagementFeatures.VehicleAlls.Constants;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Update;

public class UpdateVehicleAllCommand : IRequest<UpdatedVehicleAllResponse>
{
    public Guid Gid { get; set; }
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



    public class UpdateVehicleAllCommandHandler : IRequestHandler<UpdateVehicleAllCommand, UpdatedVehicleAllResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleAllWriteRepository _vehicleAllWriteRepository;
        private readonly IVehicleAllReadRepository _vehicleAllReadRepository;
        private readonly VehicleAllBusinessRules _vehicleAllBusinessRules;

        public UpdateVehicleAllCommandHandler(IMapper mapper, IVehicleAllWriteRepository vehicleAllWriteRepository,
                                         VehicleAllBusinessRules vehicleAllBusinessRules, IVehicleAllReadRepository vehicleAllReadRepository)
        {
            _mapper = mapper;
            _vehicleAllWriteRepository = vehicleAllWriteRepository;
            _vehicleAllBusinessRules = vehicleAllBusinessRules;
            _vehicleAllReadRepository = vehicleAllReadRepository;
        }

        public async Task<UpdatedVehicleAllResponse> Handle(UpdateVehicleAllCommand request, CancellationToken cancellationToken)
        {
            X.VehicleAll? vehicleAll = await _vehicleAllReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OtoBrandFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleAllBusinessRules.VehicleAllShouldExistWhenSelected(vehicleAll);
            vehicleAll = _mapper.Map(request, vehicleAll);

            _vehicleAllWriteRepository.Update(vehicleAll!);
            await _vehicleAllWriteRepository.SaveAsync();
            GetByGidVehicleAllResponse obj = _mapper.Map<GetByGidVehicleAllResponse>(vehicleAll);

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