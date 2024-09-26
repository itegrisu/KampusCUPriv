using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Create;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Update;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.ForeignLanguage, CreateForeignLanguageCommand>().ReverseMap();
        CreateMap<X.ForeignLanguage, CreatedForeignLanguageResponse>().ReverseMap();
        CreateMap<X.ForeignLanguage, UpdateForeignLanguageCommand>().ReverseMap();
        CreateMap<X.ForeignLanguage, UpdatedForeignLanguageResponse>().ReverseMap();
        CreateMap<X.ForeignLanguage, DeleteForeignLanguageCommand>().ReverseMap();
        CreateMap<X.ForeignLanguage, DeletedForeignLanguageResponse>().ReverseMap();

        CreateMap<X.ForeignLanguage, GetByGidForeignLanguageResponse>().ReverseMap();

        CreateMap<X.ForeignLanguage, GetListForeignLanguageListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.ForeignLanguage>, GetListResponse<GetListForeignLanguageListItemDto>>().ReverseMap();
    }
}