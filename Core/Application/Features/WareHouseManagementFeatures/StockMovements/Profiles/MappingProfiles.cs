using Application.Features.WareHouseManagementFeatures.StockMovements.Commands.Create;
using Application.Features.WareHouseManagementFeatures.StockMovements.Commands.Delete;
using Application.Features.WareHouseManagementFeatures.StockMovements.Commands.Update;
using Application.Features.WareHouseManagementFeatures.StockMovements.Queries.GetByGid;
using Application.Features.WareHouseManagementFeatures.StockMovements.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.WarehouseManagements;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StockMovement, CreateStockMovementCommand>().ReverseMap();
        CreateMap<StockMovement, CreatedStockMovementResponse>().ReverseMap();
        CreateMap<StockMovement, UpdateStockMovementCommand>().ReverseMap();
        CreateMap<StockMovement, UpdatedStockMovementResponse>().ReverseMap();
        CreateMap<StockMovement, DeleteStockMovementCommand>().ReverseMap();
        CreateMap<StockMovement, DeletedStockMovementResponse>().ReverseMap();

        CreateMap<StockMovement, GetByGidStockMovementResponse>().ReverseMap();

        CreateMap<StockMovement, GetListStockMovementListItemDto>().ReverseMap();
        CreateMap<IPaginate<StockMovement>, GetListResponse<GetListStockMovementListItemDto>>().ReverseMap();
    }
}