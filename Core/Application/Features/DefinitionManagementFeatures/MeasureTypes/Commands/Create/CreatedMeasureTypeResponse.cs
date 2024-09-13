using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Create;

public class CreatedMeasureTypeResponse : BaseResponse, IResponse
{
    public GetByGidMeasureTypeResponse Obj { get; set; }
}