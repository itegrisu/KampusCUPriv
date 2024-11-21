using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Update;

public class UpdatedDistrictResponse : BaseResponse, IResponse
{
    public GetByGidDistrictResponse Obj { get; set; }
}