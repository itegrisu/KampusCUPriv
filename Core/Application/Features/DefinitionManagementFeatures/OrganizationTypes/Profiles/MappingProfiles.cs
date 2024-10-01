using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OrganizationType, CreateOrganizationTypeCommand>().ReverseMap();
        CreateMap<X.OrganizationType, CreatedOrganizationTypeResponse>().ReverseMap();
        CreateMap<X.OrganizationType, UpdateOrganizationTypeCommand>().ReverseMap();
        CreateMap<X.OrganizationType, UpdatedOrganizationTypeResponse>().ReverseMap();
        CreateMap<X.OrganizationType, DeleteOrganizationTypeCommand>().ReverseMap();
        CreateMap<X.OrganizationType, DeletedOrganizationTypeResponse>().ReverseMap();

		CreateMap<X.OrganizationType, GetByGidOrganizationTypeResponse>().ReverseMap();

        CreateMap<X.OrganizationType, GetListOrganizationTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationType>, GetListResponse<GetListOrganizationTypeListItemDto>>().ReverseMap();
    }
}