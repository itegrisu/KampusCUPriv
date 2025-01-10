using Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Create;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Delete;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Update;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.AnnouncementType, CreateAnnouncementTypeCommand>().ReverseMap();
        CreateMap<X.AnnouncementType, CreatedAnnouncementTypeResponse>().ReverseMap();
        CreateMap<X.AnnouncementType, UpdateAnnouncementTypeCommand>().ReverseMap();
        CreateMap<X.AnnouncementType, UpdatedAnnouncementTypeResponse>().ReverseMap();
        CreateMap<X.AnnouncementType, DeleteAnnouncementTypeCommand>().ReverseMap();
        CreateMap<X.AnnouncementType, DeletedAnnouncementTypeResponse>().ReverseMap();

		CreateMap<X.AnnouncementType, GetByGidAnnouncementTypeResponse>().ReverseMap();

        CreateMap<X.AnnouncementType, GetListAnnouncementTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.AnnouncementType>, GetListResponse<GetListAnnouncementTypeListItemDto>>().ReverseMap();
    }
}