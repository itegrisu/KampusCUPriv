using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByOrganizationGid;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OrganizationFile, CreateOrganizationFileCommand>().ReverseMap();
        CreateMap<X.OrganizationFile, CreatedOrganizationFileResponse>().ReverseMap();
        CreateMap<X.OrganizationFile, UpdateOrganizationFileCommand>().ReverseMap();
        CreateMap<X.OrganizationFile, UpdatedOrganizationFileResponse>().ReverseMap();
        CreateMap<X.OrganizationFile, DeleteOrganizationFileCommand>().ReverseMap();
        CreateMap<X.OrganizationFile, DeletedOrganizationFileResponse>().ReverseMap();

		CreateMap<X.OrganizationFile, GetByGidOrganizationFileResponse>().ReverseMap();

        CreateMap<X.OrganizationFile, GetListOrganizationFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationFile>, GetListResponse<GetListOrganizationFileListItemDto>>().ReverseMap();


        CreateMap<X.OrganizationFile, GetByOrganizationGidListOrganizationFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OrganizationFile>, GetListResponse<GetByOrganizationGidListOrganizationFileListItemDto>>().ReverseMap();
    }
}