using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OrganizationItem, CreateOrganizationItemCommand>().ReverseMap();
        CreateMap<X.OrganizationItem, CreatedOrganizationItemResponse>().ReverseMap();
        CreateMap<X.OrganizationItem, UpdateOrganizationItemCommand>().ReverseMap();
        CreateMap<X.OrganizationItem, UpdatedOrganizationItemResponse>().ReverseMap();
        CreateMap<X.OrganizationItem, DeleteOrganizationItemCommand>().ReverseMap();
        CreateMap<X.OrganizationItem, DeletedOrganizationItemResponse>().ReverseMap();

		CreateMap<X.OrganizationItem, GetByGidOrganizationItemResponse>().ReverseMap();

        CreateMap<X.OrganizationItem, GetListOrganizationItemListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationItem>, GetListResponse<GetListOrganizationItemListItemDto>>().ReverseMap();
    }
}