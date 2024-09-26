using Application.Features.DefinationManagementFeatures.Warehouses.Commands.Create;
using Application.Features.DefinationManagementFeatures.Warehouses.Commands.Delete;
using Application.Features.DefinationManagementFeatures.Warehouses.Commands.Update;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Warehouse, CreateWarehouseCommand>().ReverseMap();
        CreateMap<Warehouse, CreatedWarehouseResponse>().ReverseMap();
        CreateMap<Warehouse, UpdateWarehouseCommand>().ReverseMap();
        CreateMap<Warehouse, UpdatedWarehouseResponse>().ReverseMap();
        CreateMap<Warehouse, DeleteWarehouseCommand>().ReverseMap();
        CreateMap<Warehouse, DeletedWarehouseResponse>().ReverseMap();

		CreateMap<Warehouse, GetByGidWarehouseResponse>().ReverseMap();

        CreateMap<Warehouse, GetListWarehouseListItemDto>().ReverseMap();
        CreateMap<IPaginate<Warehouse>, GetListResponse<GetListWarehouseListItemDto>>().ReverseMap();
    }
}