using Application.Features.PortalManagementFeatures.PortalTexts.Commands.Create;
using Application.Features.PortalManagementFeatures.PortalTexts.Commands.Delete;
using Application.Features.PortalManagementFeatures.PortalTexts.Commands.Update;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PortalManagements;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PortalText, CreatePortalTextCommand>().ReverseMap();
        CreateMap<X.PortalText, CreatedPortalTextResponse>().ReverseMap();
        CreateMap<X.PortalText, UpdatePortalTextCommand>().ReverseMap();
        CreateMap<X.PortalText, UpdatedPortalTextResponse>().ReverseMap();
        CreateMap<X.PortalText, DeletePortalTextCommand>().ReverseMap();
        CreateMap<X.PortalText, DeletedPortalTextResponse>().ReverseMap();

		CreateMap<X.PortalText, GetByGidPortalTextResponse>().ReverseMap();

        CreateMap<X.PortalText, GetListPortalTextListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PortalText>, GetListResponse<GetListPortalTextListItemDto>>().ReverseMap();
    }
}