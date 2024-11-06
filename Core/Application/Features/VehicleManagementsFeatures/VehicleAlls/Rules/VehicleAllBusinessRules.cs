using Application.Features.VehicleManagementFeatures.VehicleAlls.Constants;
using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Rules;

public class VehicleAllBusinessRules : BaseBusinessRules
{
    private readonly IVehicleAllReadRepository _vehicleAllRepository;

    public VehicleAllBusinessRules(IVehicleAllReadRepository vehicleAllRepository)
    {
        _vehicleAllRepository = vehicleAllRepository;
    }

    public async Task VehicleAllShouldExistWhenSelected(X.VehicleAll? item)
    {
        if (item == null)
            throw new BusinessException(VehicleAllsBusinessMessages.VehicleAllNotExists);
    }

    public async Task PlateNumberAllreadyExist(string plateNumber)
    {
        var vehicleAll = await _vehicleAllRepository.GetSingleAsync(x => x.PlateNumber == plateNumber);
        if (vehicleAll != null)
            throw new BusinessException(VehicleAllsBusinessMessages.PlateNumberAllreadyExist);
    }

}