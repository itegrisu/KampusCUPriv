using Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Create;
using Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Delete;
using Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Update;
using Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetByGid;
using Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Warehouse, CreateWarehouseCommand>().ReverseMap();
        CreateMap<X.Warehouse, CreatedWarehouseResponse>().ReverseMap();
        CreateMap<X.Warehouse, UpdateWarehouseCommand>().ReverseMap();
        CreateMap<X.Warehouse, UpdatedWarehouseResponse>().ReverseMap();
        CreateMap<X.Warehouse, DeleteWarehouseCommand>().ReverseMap();
        CreateMap<X.Warehouse, DeletedWarehouseResponse>().ReverseMap();

		CreateMap<X.Warehouse, GetByGidWarehouseResponse>().ReverseMap();

        CreateMap<X.Warehouse, GetListWarehouseListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Warehouse>, GetListResponse<GetListWarehouseListItemDto>>().ReverseMap();
    }
}