using Application.Features.Base;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Update;

public class UpdatedTaskResponse : BaseResponse, IResponse
{
    public GetByGidTaskResponse Obj { get; set; }
}