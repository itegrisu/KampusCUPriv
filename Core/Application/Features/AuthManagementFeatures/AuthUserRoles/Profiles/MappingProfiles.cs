using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Create;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.CreateByCheckBox;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetList;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetListByUserGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuthUserRole, CreateAuthUserRoleCommand>().ReverseMap();
        CreateMap<AuthUserRole, CreateByCheckBoxAuthUserRoleCommand>().ReverseMap();
        CreateMap<AuthUserRole, CreatedAuthUserRoleResponse>().ReverseMap();
        CreateMap<AuthUserRole, CreatedByCheckBoxAuthUserRoleResponse>().ReverseMap();
        CreateMap<AuthUserRole, UpdateAuthUserRoleCommand>().ReverseMap();
        CreateMap<AuthUserRole, UpdatedAuthUserRoleResponse>().ReverseMap();
        CreateMap<AuthUserRole, DeleteAuthUserRoleCommand>().ReverseMap();
        CreateMap<AuthUserRole, DeletedAuthUserRoleResponse>().ReverseMap();
        CreateMap<AuthUserRole, GetByGidAuthUserRoleResponse>().ReverseMap();
        CreateMap<AuthUserRole, GetListByUserGidAuthUserRoleListItemDto>().ReverseMap();
        CreateMap<IPaginate<AuthUserRole>, GetListResponse<GetListByUserGidAuthUserRoleListItemDto>>().ReverseMap();
        CreateMap<AuthUserRole, GetListAuthUserRoleListItemDto>().ReverseMap();
        CreateMap<IPaginate<AuthUserRole>, GetListResponse<GetListAuthUserRoleListItemDto>>().ReverseMap();
    }
}