using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Create;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Update;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PartTimeWorkerForeignLanguage, CreatePartTimeWorkerForeignLanguageCommand>().ReverseMap();
        CreateMap<X.PartTimeWorkerForeignLanguage, CreatedPartTimeWorkerForeignLanguageResponse>().ReverseMap();
        CreateMap<X.PartTimeWorkerForeignLanguage, UpdatePartTimeWorkerForeignLanguageCommand>().ReverseMap();
        CreateMap<X.PartTimeWorkerForeignLanguage, UpdatedPartTimeWorkerForeignLanguageResponse>().ReverseMap();
        CreateMap<X.PartTimeWorkerForeignLanguage, DeletePartTimeWorkerForeignLanguageCommand>().ReverseMap();
        CreateMap<X.PartTimeWorkerForeignLanguage, DeletedPartTimeWorkerForeignLanguageResponse>().ReverseMap();

		CreateMap<X.PartTimeWorkerForeignLanguage, GetByGidPartTimeWorkerForeignLanguageResponse>().ReverseMap();

        CreateMap<X.PartTimeWorkerForeignLanguage, GetListPartTimeWorkerForeignLanguageListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PartTimeWorkerForeignLanguage>, GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto>>().ReverseMap();
    }
}