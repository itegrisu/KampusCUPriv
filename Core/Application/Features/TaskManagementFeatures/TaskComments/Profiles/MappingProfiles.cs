using Application.Features.TaskManagementFeatures.TaskComments.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskComments.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskComments.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByTaskGid;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByTaskUserGid;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskComments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TaskComment, CreateTaskCommentCommand>().ReverseMap();
        CreateMap<TaskComment, CreatedTaskCommentResponse>().ReverseMap();
        CreateMap<TaskComment, UpdateTaskCommentCommand>().ReverseMap();
        CreateMap<TaskComment, UpdatedTaskCommentResponse>().ReverseMap();
        CreateMap<TaskComment, DeleteTaskCommentCommand>().ReverseMap();
        CreateMap<TaskComment, DeletedTaskCommentResponse>().ReverseMap();
        CreateMap<TaskComment, GetByGidTaskCommentResponse>().ReverseMap();
        CreateMap<TaskComment, GetByTaskGidTaskCommentResponse>().ReverseMap();
        CreateMap<TaskComment, GetByTaskUserGidTaskCommentResponse>().ReverseMap();


        CreateMap<TaskComment, GetListTaskCommentListItemDto>().ReverseMap();
        CreateMap<IPaginate<TaskComment>, GetListResponse<GetListTaskCommentListItemDto>>().ReverseMap();
        CreateMap<IPaginate<TaskComment>, GetListResponse<GetByTaskGidTaskCommentResponse>>().ReverseMap();
        CreateMap<IPaginate<TaskComment>, GetListResponse<GetByTaskUserGidTaskCommentResponse>>().ReverseMap();
        CreateMap<IPaginate<TaskComment>, GetListResponse<GetByGidTaskCommentResponse>>().ReverseMap();

    }
}