using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OrganizationItemMessage, CreateOrganizationItemMessageCommand>().ReverseMap();
        CreateMap<X.OrganizationItemMessage, CreatedOrganizationItemMessageResponse>().ReverseMap();
        CreateMap<X.OrganizationItemMessage, UpdateOrganizationItemMessageCommand>().ReverseMap();
        CreateMap<X.OrganizationItemMessage, UpdatedOrganizationItemMessageResponse>().ReverseMap();
        CreateMap<X.OrganizationItemMessage, DeleteOrganizationItemMessageCommand>().ReverseMap();
        CreateMap<X.OrganizationItemMessage, DeletedOrganizationItemMessageResponse>().ReverseMap();

		CreateMap<X.OrganizationItemMessage, GetByGidOrganizationItemMessageResponse>().ReverseMap();

        CreateMap<X.OrganizationItemMessage, GetListOrganizationItemMessageListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationItemMessage>, GetListResponse<GetListOrganizationItemMessageListItemDto>>().ReverseMap();
    }
}