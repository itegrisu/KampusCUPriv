using Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGroupGid;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TaskGroupUser, CreateTaskGroupUserCommand>().ReverseMap();
        CreateMap<X.TaskGroupUser, CreatedTaskGroupUserResponse>().ReverseMap();
        CreateMap<X.TaskGroupUser, UpdateTaskGroupUserCommand>().ReverseMap();
        CreateMap<X.TaskGroupUser, UpdatedTaskGroupUserResponse>().ReverseMap();
        CreateMap<X.TaskGroupUser, DeleteTaskGroupUserCommand>().ReverseMap();
        CreateMap<X.TaskGroupUser, DeletedTaskGroupUserResponse>().ReverseMap();

        CreateMap<X.TaskGroupUser, GetListTaskGroupUserListItemDto>().ReverseMap();
        CreateMap<X.TaskGroupUser, GetByGidTaskGroupUserResponse>().ReverseMap();
        CreateMap<X.TaskGroupUser, GetByGroupGidTaskGroupUserResponse>().ReverseMap();

        CreateMap<IPaginate<X.TaskGroupUser>, GetListResponse<GetListTaskGroupUserListItemDto>>().ReverseMap();
        CreateMap<IPaginate<X.TaskGroupUser>, GetListResponse<GetByGroupGidTaskGroupUserResponse>>().ReverseMap();
    }
}