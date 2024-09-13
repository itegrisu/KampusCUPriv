using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Update;

public class UpdatedPermitTypeResponse : BaseResponse, IResponse
{
    public GetByGidPermitTypeResponse Obj { get; set; }
}