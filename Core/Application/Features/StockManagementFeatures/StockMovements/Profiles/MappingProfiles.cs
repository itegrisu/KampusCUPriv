using Application.Features.StockManagementFeatures.StockMovements.Commands.Create;
using Application.Features.StockManagementFeatures.StockMovements.Commands.Delete;
using Application.Features.StockManagementFeatures.StockMovements.Commands.Update;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockMovements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.StockMovement, CreateStockMovementCommand>().ReverseMap();
        CreateMap<X.StockMovement, CreatedStockMovementResponse>().ReverseMap();
        CreateMap<X.StockMovement, UpdateStockMovementCommand>().ReverseMap();
        CreateMap<X.StockMovement, UpdatedStockMovementResponse>().ReverseMap();
        CreateMap<X.StockMovement, DeleteStockMovementCommand>().ReverseMap();
        CreateMap<X.StockMovement, DeletedStockMovementResponse>().ReverseMap();

		CreateMap<X.StockMovement, GetByGidStockMovementResponse>().ReverseMap();

        CreateMap<X.StockMovement, GetListStockMovementListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StockMovement>, GetListResponse<GetListStockMovementListItemDto>>().ReverseMap();
    }
}