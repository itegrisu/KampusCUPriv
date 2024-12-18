using Application.Features.VehicleManagementFeatures.Tyres.Commands.Create;
using Application.Features.VehicleManagementFeatures.Tyres.Commands.Delete;
using Application.Features.VehicleManagementFeatures.Tyres.Commands.Update;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.Tyres.Queries.GetByVehicleGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleTyre, CreateVehicleTyreCommand>().ReverseMap();
        CreateMap<X.VehicleTyre, CreatedVehicleTyreResponse>().ReverseMap();
        CreateMap<X.VehicleTyre, UpdateVehicleTyreCommand>().ReverseMap();
        CreateMap<X.VehicleTyre, UpdatedVehicleTyreResponse>().ReverseMap();
        CreateMap<X.VehicleTyre, DeleteVehicleTyreCommand>().ReverseMap();
        CreateMap<X.VehicleTyre, DeletedVehicleTyreResponse>().ReverseMap();

		CreateMap<X.VehicleTyre, GetByGidVehicleTyreResponse>().ReverseMap();

        CreateMap<X.VehicleTyre, GetListVehicleTyreListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleTyre>, GetListResponse<GetListVehicleTyreListItemDto>>().ReverseMap();

        CreateMap<X.VehicleTyre, GetByVehicleGidListVehicleTyreListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleTyre>, GetListResponse<GetByVehicleGidListVehicleTyreListItemDto>>().ReverseMap();
    }
}