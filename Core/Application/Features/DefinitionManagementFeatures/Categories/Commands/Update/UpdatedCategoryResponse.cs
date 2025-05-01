using Application.Features.Base;
using Application.Features.DefinitionFeatures.Categories.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Update;

public class UpdatedCategoryResponse : BaseResponse, IResponse
{
    public GetByGidCategoryResponse Obj { get; set; }
}