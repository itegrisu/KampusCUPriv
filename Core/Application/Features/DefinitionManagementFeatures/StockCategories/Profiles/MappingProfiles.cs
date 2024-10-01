using Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Create;
using Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Update;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.StockCategory, CreateStockCategoryCommand>().ReverseMap();
        CreateMap<X.StockCategory, CreatedStockCategoryResponse>().ReverseMap();
        CreateMap<X.StockCategory, UpdateStockCategoryCommand>().ReverseMap();
        CreateMap<X.StockCategory, UpdatedStockCategoryResponse>().ReverseMap();
        CreateMap<X.StockCategory, DeleteStockCategoryCommand>().ReverseMap();
        CreateMap<X.StockCategory, DeletedStockCategoryResponse>().ReverseMap();

		CreateMap<X.StockCategory, GetByGidStockCategoryResponse>().ReverseMap();

        CreateMap<X.StockCategory, GetListStockCategoryListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StockCategory>, GetListResponse<GetListStockCategoryListItemDto>>().ReverseMap();
    }
}