using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Create;

public class CreatedPermitTypeResponse : BaseResponse, IResponse
{
    public GetByGidPermitTypeResponse Obj { get; set; }
}