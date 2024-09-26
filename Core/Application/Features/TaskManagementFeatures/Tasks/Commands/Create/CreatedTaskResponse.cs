using Application.Features.Base;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Create;

public class CreatedTaskResponse : BaseResponse, IResponse
{
    public GetByGidTaskResponse Obj { get; set; }
}