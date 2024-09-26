using Application.Features.TaskManagementFeatures.TaskUsers.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskUsers.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskUsers.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByUserGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetList;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetListByTaskGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TaskUser, CreateTaskUserCommand>().ReverseMap();
        CreateMap<X.TaskUser, CreatedTaskUserResponse>().ReverseMap();
        CreateMap<X.TaskUser, UpdateTaskUserCommand>().ReverseMap();
        CreateMap<X.TaskUser, UpdatedTaskUserResponse>().ReverseMap();
        CreateMap<X.TaskUser, DeleteTaskUserCommand>().ReverseMap();
        CreateMap<X.TaskUser, DeletedTaskUserResponse>().ReverseMap();


        CreateMap<X.TaskUser, GetListTaskUserListItemDto>().ReverseMap();
        CreateMap<X.TaskUser, GetListByTaskGidTaskUserListItemDto>().ReverseMap();
        CreateMap<X.TaskUser, GetByGidTaskUserResponse>().ReverseMap();
        CreateMap<IPaginate<X.TaskUser>, GetListResponse<GetListTaskUserListItemDto>>().ReverseMap();
        CreateMap<IPaginate<X.TaskUser>, GetListResponse<GetListByTaskGidTaskUserListItemDto>>().ReverseMap();
        CreateMap<X.TaskUser, GetByUserGidTaskUserResponse>().ReverseMap();
        CreateMap<IPaginate<X.TaskUser>, GetListResponse<GetByUserGidTaskUserResponse>>().ReverseMap();
    }
}