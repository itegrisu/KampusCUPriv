using Application.Features.Base;
using Application.Features.DefinitionFeatures.Departments.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Create;

public class CreatedDepartmentResponse : BaseResponse, IResponse
{
    public GetByGidDepartmentResponse Obj { get; set; }
}