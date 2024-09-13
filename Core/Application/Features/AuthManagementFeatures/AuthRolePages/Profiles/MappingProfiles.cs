using Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Create;
using Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuthRolePage, CreateAuthRolePageCommand>().ReverseMap();
        CreateMap<AuthRolePage, CreatedAuthRolePageResponse>().ReverseMap();
        CreateMap<AuthRolePage, UpdateAuthRolePageCommand>().ReverseMap();
        CreateMap<AuthRolePage, UpdatedAuthRolePageResponse>().ReverseMap();
        CreateMap<AuthRolePage, DeleteAuthRolePageCommand>().ReverseMap();
        CreateMap<AuthRolePage, DeletedAuthRolePageResponse>().ReverseMap();
        CreateMap<AuthRolePage, GetByGidAuthRolePageResponse>().ReverseMap();
        CreateMap<AuthRolePage, GetListAuthRolePageListItemDto>().ReverseMap();
        CreateMap<IPaginate<AuthRolePage>, GetListResponse<GetListAuthRolePageListItemDto>>().ReverseMap();
    }
}