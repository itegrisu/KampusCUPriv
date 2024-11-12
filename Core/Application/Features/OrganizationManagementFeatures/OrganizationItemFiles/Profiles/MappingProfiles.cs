using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByOrganizationGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OrganizationItemFile, CreateOrganizationItemFileCommand>().ReverseMap();
        CreateMap<X.OrganizationItemFile, CreatedOrganizationItemFileResponse>().ReverseMap();
        CreateMap<X.OrganizationItemFile, UpdateOrganizationItemFileCommand>().ReverseMap();
        CreateMap<X.OrganizationItemFile, UpdatedOrganizationItemFileResponse>().ReverseMap();
        CreateMap<X.OrganizationItemFile, DeleteOrganizationItemFileCommand>().ReverseMap();
        CreateMap<X.OrganizationItemFile, DeletedOrganizationItemFileResponse>().ReverseMap();

        CreateMap<X.OrganizationItemFile, GetByGidOrganizationItemFileResponse>().ReverseMap();

        CreateMap<X.OrganizationItemFile, GetListOrganizationItemFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationItemFile>, GetListResponse<GetListOrganizationItemFileListItemDto>>().ReverseMap();

        CreateMap<X.OrganizationItemFile, GetByOrganizationGidListOrganizationItemFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationItemFile>, GetListResponse<GetByOrganizationGidListOrganizationItemFileListItemDto>>().ReverseMap();
    }
}