using Application.Features.TaskManagementFeatures.TaskFiles.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskFiles.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskFiles.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetList;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetListByTaskGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TaskFile, CreateTaskFileCommand>().ReverseMap();
        CreateMap<X.TaskFile, CreatedTaskFileResponse>().ReverseMap();
        CreateMap<X.TaskFile, UpdateTaskFileCommand>().ReverseMap();
        CreateMap<X.TaskFile, UpdatedTaskFileResponse>().ReverseMap();
        CreateMap<X.TaskFile, DeleteTaskFileCommand>().ReverseMap();
        CreateMap<X.TaskFile, DeletedTaskFileResponse>().ReverseMap();

        CreateMap<X.TaskFile, GetListTaskFileListItemDto>().ReverseMap();
        CreateMap<X.TaskFile, GetListByTaskGidTaskFileListItemDto>().ReverseMap();
        CreateMap<X.TaskFile, GetByGidTaskFileResponse>().ReverseMap();
        CreateMap<IPaginate<X.TaskFile>, GetListResponse<GetListTaskFileListItemDto>>().ReverseMap();
        CreateMap<IPaginate<X.TaskFile>, GetListResponse<GetListByTaskGidTaskFileListItemDto>>().ReverseMap();
    }
}