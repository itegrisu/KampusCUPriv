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
        CreateMap<X.Tyre, CreateTyreCommand>().ReverseMap();
        CreateMap<X.Tyre, CreatedTyreResponse>().ReverseMap();
        CreateMap<X.Tyre, UpdateTyreCommand>().ReverseMap();
        CreateMap<X.Tyre, UpdatedTyreResponse>().ReverseMap();
        CreateMap<X.Tyre, DeleteTyreCommand>().ReverseMap();
        CreateMap<X.Tyre, DeletedTyreResponse>().ReverseMap();

		CreateMap<X.Tyre, GetByGidTyreResponse>().ReverseMap();

        CreateMap<X.Tyre, GetListTyreListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Tyre>, GetListResponse<GetListTyreListItemDto>>().ReverseMap();

        CreateMap<X.Tyre, GetByVehicleGidListTyreListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Tyre>, GetListResponse<GetByVehicleGidListTyreListItemDto>>().ReverseMap();
    }
}