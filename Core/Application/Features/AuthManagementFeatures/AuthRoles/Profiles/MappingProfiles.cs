using AutoMapper;
using Domain.Entities.AuthManagements;
using Core.Persistence.Paging;
using Core.Application.Responses;
using Application.Features.AuthManagementFeatures.AuthRoles.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetList;
using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRoles.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthRoles.Commands.Create;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuthRole, CreateAuthRoleCommand>().ReverseMap();
        CreateMap<AuthRole, CreatedAuthRoleResponse>().ReverseMap();
        CreateMap<AuthRole, UpdateAuthRoleCommand>().ReverseMap();
        CreateMap<AuthRole, UpdatedAuthRoleResponse>().ReverseMap();
        CreateMap<AuthRole, DeleteAuthRoleCommand>().ReverseMap();
        CreateMap<AuthRole, DeletedAuthRoleResponse>().ReverseMap();
        CreateMap<AuthRole, GetByGidAuthRoleResponse>().ReverseMap();
        CreateMap<AuthRole, GetListAuthRoleListItemDto>().ReverseMap();
        CreateMap<IPaginate<AuthRole>, GetListResponse<GetListAuthRoleListItemDto>>().ReverseMap();
    }
}