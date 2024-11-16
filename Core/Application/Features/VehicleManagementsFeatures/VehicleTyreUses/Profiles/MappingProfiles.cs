using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleTyreUses.Queries.GetByVehicleGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleTyreUse, CreateVehicleTyreUseCommand>().ReverseMap();
        CreateMap<X.VehicleTyreUse, CreatedVehicleTyreUseResponse>().ReverseMap();
        CreateMap<X.VehicleTyreUse, UpdateVehicleTyreUseCommand>().ReverseMap();
        CreateMap<X.VehicleTyreUse, UpdatedVehicleTyreUseResponse>().ReverseMap();
        CreateMap<X.VehicleTyreUse, DeleteVehicleTyreUseCommand>().ReverseMap();
        CreateMap<X.VehicleTyreUse, DeletedVehicleTyreUseResponse>().ReverseMap();

		CreateMap<X.VehicleTyreUse, GetByGidVehicleTyreUseResponse>().ReverseMap();

        CreateMap<X.VehicleTyreUse, GetListVehicleTyreUseListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleTyreUse>, GetListResponse<GetListVehicleTyreUseListItemDto>>().ReverseMap();

        CreateMap<X.VehicleTyreUse, GetByVehicleGidListVehicleTyreUseListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleTyreUse>, GetListResponse<GetByVehicleGidListVehicleTyreUseListItemDto>>().ReverseMap();
    }
}