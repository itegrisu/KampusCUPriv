using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetById;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelForeignLanguage, CreatePersonnelForeignLanguageCommand>().ReverseMap();
        CreateMap<X.PersonnelForeignLanguage, CreatedPersonnelForeignLanguageResponse>().ReverseMap();
        CreateMap<X.PersonnelForeignLanguage, UpdatePersonnelForeignLanguageCommand>().ReverseMap();
        CreateMap<X.PersonnelForeignLanguage, UpdatedPersonnelForeignLanguageResponse>().ReverseMap();
        CreateMap<X.PersonnelForeignLanguage, DeletePersonnelForeignLanguageCommand>().ReverseMap();
        CreateMap<X.PersonnelForeignLanguage, DeletedPersonnelForeignLanguageResponse>().ReverseMap();

		CreateMap<X.PersonnelForeignLanguage, GetByGidPersonnelForeignLanguageResponse>().ReverseMap();

        CreateMap<X.PersonnelForeignLanguage, GetListPersonnelForeignLanguageListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelForeignLanguage>, GetListResponse<GetListPersonnelForeignLanguageListItemDto>>().ReverseMap();
    }
}