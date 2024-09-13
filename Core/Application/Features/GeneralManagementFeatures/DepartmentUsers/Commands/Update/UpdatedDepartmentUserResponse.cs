using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Update;

public class UpdatedDepartmentUserResponse : BaseResponse, IResponse
{
    public GetByGidDepartmentUserResponse Obj { get; set; }
}