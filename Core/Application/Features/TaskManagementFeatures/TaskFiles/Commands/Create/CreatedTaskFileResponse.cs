using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Create;

public class CreatedTaskFileResponse : BaseResponse, IResponse
{
    public GetByGidTaskFileResponse Obj { get; set; }
}