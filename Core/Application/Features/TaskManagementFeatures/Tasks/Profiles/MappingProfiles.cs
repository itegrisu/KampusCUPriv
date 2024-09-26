using Application.Features.TaskManagementFeatures.Tasks.Commands.Create;
using Application.Features.TaskManagementFeatures.Tasks.Commands.Delete;
using Application.Features.TaskManagementFeatures.Tasks.Commands.Update;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using T = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.Tasks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<T.Task, CreateTaskCommand>().ReverseMap();
        CreateMap<T.Task, CreatedTaskResponse>().ReverseMap();
        CreateMap<T.Task, UpdateTaskCommand>().ReverseMap();
        CreateMap<T.Task, UpdatedTaskResponse>().ReverseMap();
        CreateMap<T.Task, DeleteTaskCommand>().ReverseMap();
        CreateMap<T.Task, DeletedTaskResponse>().ReverseMap();
        CreateMap<T.Task, GetByGidTaskResponse>().ReverseMap();
        CreateMap<T.Task, GetListTaskListItemDto>().ReverseMap();
        CreateMap<IPaginate<T.Task>, GetListResponse<GetListTaskListItemDto>>().ReverseMap();
    }
}