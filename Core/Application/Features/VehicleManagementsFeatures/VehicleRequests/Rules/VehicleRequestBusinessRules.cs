using Application.Features.VehicleManagementFeatures.VehicleRequests.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Rules;

public class VehicleRequestBusinessRules : BaseBusinessRules
{
    public async Task VehicleRequestShouldExistWhenSelected(X.VehicleRequest? item)
    {
        if (item == null)
            throw new BusinessException(VehicleRequestsBusinessMessages.VehicleRequestNotExists);
    }
}