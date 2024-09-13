using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Create;

public class CreatedDepartmentUserResponse : BaseResponse, IResponse
{
    public GetByGidDepartmentUserResponse Obj { get; set; }
}