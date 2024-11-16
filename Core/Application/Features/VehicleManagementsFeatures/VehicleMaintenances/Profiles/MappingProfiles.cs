using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Queries.GetByVehicleGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleMaintenance, CreateVehicleMaintenanceCommand>().ReverseMap();
        CreateMap<X.VehicleMaintenance, CreatedVehicleMaintenanceResponse>().ReverseMap();
        CreateMap<X.VehicleMaintenance, UpdateVehicleMaintenanceCommand>().ReverseMap();
        CreateMap<X.VehicleMaintenance, UpdatedVehicleMaintenanceResponse>().ReverseMap();
        CreateMap<X.VehicleMaintenance, DeleteVehicleMaintenanceCommand>().ReverseMap();
        CreateMap<X.VehicleMaintenance, DeletedVehicleMaintenanceResponse>().ReverseMap();

		CreateMap<X.VehicleMaintenance, GetByGidVehicleMaintenanceResponse>().ReverseMap();

        CreateMap<X.VehicleMaintenance, GetListVehicleMaintenanceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleMaintenance>, GetListResponse<GetListVehicleMaintenanceListItemDto>>().ReverseMap();

        CreateMap<X.VehicleMaintenance, GetByVehicleGidListVehicleMaintenanceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleMaintenance>, GetListResponse<GetByVehicleGidListVehicleMaintenanceListItemDto>>().ReverseMap();
    }
}