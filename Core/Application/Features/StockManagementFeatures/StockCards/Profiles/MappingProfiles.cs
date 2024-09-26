using Application.Features.StockManagementFeatures.StockCards.Commands.Create;
using Application.Features.StockManagementFeatures.StockCards.Commands.Delete;
using Application.Features.StockManagementFeatures.StockCards.Commands.Update;
using Application.Features.StockManagementFeatures.StockCards.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockCards.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockCards.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.StockCard, CreateStockCardCommand>().ReverseMap();
        CreateMap<X.StockCard, CreatedStockCardResponse>().ReverseMap();
        CreateMap<X.StockCard, UpdateStockCardCommand>().ReverseMap();
        CreateMap<X.StockCard, UpdatedStockCardResponse>().ReverseMap();
        CreateMap<X.StockCard, DeleteStockCardCommand>().ReverseMap();
        CreateMap<X.StockCard, DeletedStockCardResponse>().ReverseMap();

		CreateMap<X.StockCard, GetByGidStockCardResponse>().ReverseMap();

        CreateMap<X.StockCard, GetListStockCardListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StockCard>, GetListResponse<GetListStockCardListItemDto>>().ReverseMap();
    }
}