using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Update;

public class UpdatedOtoBrandResponse : BaseResponse, IResponse
{
    public GetByGidOtoBrandResponse Obj { get; set; }
}