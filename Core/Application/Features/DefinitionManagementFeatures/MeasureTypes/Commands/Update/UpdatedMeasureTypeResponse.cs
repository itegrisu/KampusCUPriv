using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Update;

public class UpdatedMeasureTypeResponse : BaseResponse, IResponse
{
    public GetByGidMeasureTypeResponse Obj { get; set; }
}