using Application.Features.TaskManagementFeatures.TaskManagers.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskManagers.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskManagers.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TaskManager, CreateTaskManagerCommand>().ReverseMap();
        CreateMap<X.TaskManager, CreatedTaskManagerResponse>().ReverseMap();
        CreateMap<X.TaskManager, UpdateTaskManagerCommand>().ReverseMap();
        CreateMap<X.TaskManager, UpdatedTaskManagerResponse>().ReverseMap();
        CreateMap<X.TaskManager, DeleteTaskManagerCommand>().ReverseMap();
        CreateMap<X.TaskManager, DeletedTaskManagerResponse>().ReverseMap();

        CreateMap<X.TaskManager, GetListTaskManagerListItemDto>().ReverseMap();
        CreateMap<X.TaskManager, GetByGidTaskManagerResponse>().ReverseMap();
        CreateMap<IPaginate<X.TaskManager>, GetListResponse<GetListTaskManagerListItemDto>>().ReverseMap();
    }
}