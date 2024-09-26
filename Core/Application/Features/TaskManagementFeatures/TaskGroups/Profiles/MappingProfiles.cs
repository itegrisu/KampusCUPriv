using Application.Features.TaskManagementFeatures.TaskGroups.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskGroups.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskGroups.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TaskGroup, CreateTaskGroupCommand>().ReverseMap();
        CreateMap<X.TaskGroup, CreatedTaskGroupResponse>().ReverseMap();
        CreateMap<X.TaskGroup, UpdateTaskGroupCommand>().ReverseMap();
        CreateMap<X.TaskGroup, UpdatedTaskGroupResponse>().ReverseMap();
        CreateMap<X.TaskGroup, DeleteTaskGroupCommand>().ReverseMap();
        CreateMap<X.TaskGroup, DeletedTaskGroupResponse>().ReverseMap();

        //CreateMap<X.TaskGroup, GetByIdTaskGroupResponse>().ReverseMap();
        //CreateMap<X.TaskGroup, GetByGidTaskGroupResponse>().ReverseMap();
        //CreateMap<X.TaskGroup, GetListTaskGroupListItemDto>().ReverseMap();
        //CreateMap<IPaginate<X.TaskGroup>, GetListResponse<GetListTaskGroupListItemDto>>().ReverseMap();


        CreateMap<X.TaskGroup, GetByGidTaskGroupResponse>().ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.TaskGroupUsers
        .Where(t => t.DataState == Core.Enum.DataState.Active).Count())).ReverseMap();

        CreateMap<X.TaskGroup, GetListTaskGroupListItemDto>()
    .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.TaskGroupUsers.Where(t => t.DataState == Core.Enum.DataState.Active).Count()))
    .ReverseMap();

        CreateMap<IPaginate<X.TaskGroup>, GetListResponse<GetListTaskGroupListItemDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}