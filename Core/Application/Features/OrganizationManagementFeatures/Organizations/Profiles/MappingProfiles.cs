using Application.Features.OrganizationManagementFeatures.Organizations.Commands.Create;
using Application.Features.OrganizationManagementFeatures.Organizations.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.Organizations.Commands.Update;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Organization, CreateOrganizationCommand>().ReverseMap();
        CreateMap<X.Organization, CreatedOrganizationResponse>().ReverseMap();
        CreateMap<X.Organization, UpdateOrganizationCommand>().ReverseMap();
        CreateMap<X.Organization, UpdatedOrganizationResponse>().ReverseMap();
        CreateMap<X.Organization, DeleteOrganizationCommand>().ReverseMap();
        CreateMap<X.Organization, DeletedOrganizationResponse>().ReverseMap();

		CreateMap<X.Organization, GetByGidOrganizationResponse>().ReverseMap();

        CreateMap<X.Organization, GetListOrganizationListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Organization>, GetListResponse<GetListOrganizationListItemDto>>().ReverseMap();
    }
}