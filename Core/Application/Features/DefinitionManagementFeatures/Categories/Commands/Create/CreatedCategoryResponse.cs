using Application.Features.Base;
using Application.Features.DefinitionFeatures.Categories.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Create;

public class CreatedCategoryResponse : BaseResponse, IResponse
{
    public GetByGidCategoryResponse Obj { get; set; }
}