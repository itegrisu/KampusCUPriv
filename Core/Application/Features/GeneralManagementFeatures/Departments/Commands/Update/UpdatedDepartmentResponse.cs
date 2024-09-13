using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Update;

public class UpdatedDepartmentResponse : BaseResponse, IResponse
{
    public GetByGidDepartmentResponse Obj { get; set; }
}