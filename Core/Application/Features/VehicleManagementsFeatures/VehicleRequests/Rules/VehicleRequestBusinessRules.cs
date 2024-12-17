using Application.Features.VehicleManagementFeatures.VehicleRequests.Constants;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Enums;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Rules;

public class VehicleRequestBusinessRules : BaseBusinessRules
{
    private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
    private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;

    public VehicleRequestBusinessRules(IVehicleRequestReadRepository vehicleRequestReadRepository, ITransportationServiceReadRepository transportationServiceReadRepository)
    {
        _vehicleRequestReadRepository = vehicleRequestReadRepository;
        _transportationServiceReadRepository = transportationServiceReadRepository;
    }

    public async Task VehicleRequestShouldExistWhenSelected(X.VehicleRequest? item)
    {
        if (item == null)
            throw new BusinessException(VehicleRequestsBusinessMessages.VehicleRequestNotExists);
    }

    public async Task<bool> CheckVehicleAvailability(Guid gidVehicleFK, DateTime startDate, DateTime endDate)
    {
        // Onaylanmýþ araç taleplerini kontrol ediyoruz
        var conflictingRequests = await _vehicleRequestReadRepository.GetListAsync(
            predicate: x => x.GidVehicleFK == gidVehicleFK &&
                            x.VehicleApprovedStatus == EnumVehicleApprovedStatus.Onaylandi &&
                            x.StartDate < endDate &&
                            x.EndDate > startDate // Tarihler çakýþýyor mu
        );

        if (conflictingRequests.Items.Any())
        {
            return false; // Çakýþma var
        }

        // TransportationService içinde çakýþma var mý kontrol ediyoruz
        var conflictingTransportationServices = await _transportationServiceReadRepository.GetListAsync(
            predicate: x => x.GidVehicleFK == gidVehicleFK &&
                            x.StartDate < endDate &&
                            x.EndDate > startDate // Tarihler çakýþýyor mu
        );

        if (conflictingTransportationServices.Items.Any())
        {
            return false; // Çakýþma var
        }

        return true; // Araç uygun
    }

}