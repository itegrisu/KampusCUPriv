using Application.Features.Base;
using Application.Features.DefinitionFeatures.Departments.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Update;

public class UpdatedDepartmentResponse : BaseResponse, IResponse
{
    public GetByGidDepartmentResponse Obj { get; set; }
}