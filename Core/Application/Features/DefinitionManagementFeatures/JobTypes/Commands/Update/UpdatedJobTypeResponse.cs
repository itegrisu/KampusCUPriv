using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Update;

public class UpdatedJobTypeResponse : BaseResponse, IResponse
{
    public GetByGidJobTypeResponse Obj { get; set; }
}