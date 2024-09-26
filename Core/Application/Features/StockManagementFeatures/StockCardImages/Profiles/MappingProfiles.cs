using Application.Features.StockManagementFeatures.StockCardImages.Commands.Create;
using Application.Features.StockManagementFeatures.StockCardImages.Commands.Delete;
using Application.Features.StockManagementFeatures.StockCardImages.Commands.Update;
using Application.Features.StockManagementFeatures.StockCardImages.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockCardImages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockCardImages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.StockCardImage, CreateStockCardImageCommand>().ReverseMap();
        CreateMap<X.StockCardImage, CreatedStockCardImageResponse>().ReverseMap();
        CreateMap<X.StockCardImage, UpdateStockCardImageCommand>().ReverseMap();
        CreateMap<X.StockCardImage, UpdatedStockCardImageResponse>().ReverseMap();
        CreateMap<X.StockCardImage, DeleteStockCardImageCommand>().ReverseMap();
        CreateMap<X.StockCardImage, DeletedStockCardImageResponse>().ReverseMap();

		CreateMap<X.StockCardImage, GetByGidStockCardImageResponse>().ReverseMap();

        CreateMap<X.StockCardImage, GetListStockCardImageListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StockCardImage>, GetListResponse<GetListStockCardImageListItemDto>>().ReverseMap();
    }
}