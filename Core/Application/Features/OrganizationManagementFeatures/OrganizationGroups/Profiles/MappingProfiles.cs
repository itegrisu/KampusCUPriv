using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OrganizationGroup, CreateOrganizationGroupCommand>().ReverseMap();
        CreateMap<X.OrganizationGroup, CreatedOrganizationGroupResponse>().ReverseMap();
        CreateMap<X.OrganizationGroup, UpdateOrganizationGroupCommand>().ReverseMap();
        CreateMap<X.OrganizationGroup, UpdatedOrganizationGroupResponse>().ReverseMap();
        CreateMap<X.OrganizationGroup, DeleteOrganizationGroupCommand>().ReverseMap();
        CreateMap<X.OrganizationGroup, DeletedOrganizationGroupResponse>().ReverseMap();

		CreateMap<X.OrganizationGroup, GetByGidOrganizationGroupResponse>().ReverseMap();

        CreateMap<X.OrganizationGroup, GetListOrganizationGroupListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationGroup>, GetListResponse<GetListOrganizationGroupListItemDto>>().ReverseMap();
    }
}