using Application.Features.DefinitionFeatures.Categories.Commands.Create;
using Application.Features.DefinitionFeatures.Categories.Commands.Delete;
using Application.Features.DefinitionFeatures.Categories.Commands.Update;
using Application.Features.DefinitionFeatures.Categories.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Categories.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.Categories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<X.Category, CreatedCategoryResponse>().ReverseMap();
        CreateMap<X.Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<X.Category, UpdatedCategoryResponse>().ReverseMap();
        CreateMap<X.Category, DeleteCategoryCommand>().ReverseMap();
        CreateMap<X.Category, DeletedCategoryResponse>().ReverseMap();

		CreateMap<X.Category, GetByGidCategoryResponse>().ReverseMap();

        CreateMap<X.Category, GetListCategoryListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Category>, GetListResponse<GetListCategoryListItemDto>>().ReverseMap();
    }
}