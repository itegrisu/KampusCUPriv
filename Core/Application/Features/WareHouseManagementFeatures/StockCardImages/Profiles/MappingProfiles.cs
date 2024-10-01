using Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Create;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Delete;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Update;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetByGid;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.WarehouseManagements;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<StockCardImage, CreateStockCardImageCommand>().ReverseMap();
        CreateMap<StockCardImage, CreatedStockCardImageResponse>().ReverseMap();
        CreateMap<StockCardImage, UpdateStockCardImageCommand>().ReverseMap();
        CreateMap<StockCardImage, UpdatedStockCardImageResponse>().ReverseMap();
        CreateMap<StockCardImage, DeleteStockCardImageCommand>().ReverseMap();
        CreateMap<StockCardImage, DeletedStockCardImageResponse>().ReverseMap();

        CreateMap<StockCardImage, GetByGidStockCardImageResponse>().ReverseMap();

        CreateMap<StockCardImage, GetListStockCardImageListItemDto>().ReverseMap();
        CreateMap<IPaginate<StockCardImage>, GetListResponse<GetListStockCardImageListItemDto>>().ReverseMap();
    }
}