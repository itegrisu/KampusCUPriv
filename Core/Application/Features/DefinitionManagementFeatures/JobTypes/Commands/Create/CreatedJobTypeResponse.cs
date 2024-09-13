using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Create;

public class CreatedJobTypeResponse : BaseResponse, IResponse
{
    public GetByGidJobTypeResponse Obj { get; set; }
}