using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UpdateForAdmin
{
    public class UpdateForAdminUserResponse : BaseResponse, IResponse
    {
        public GetByGidUserResponse Obj { get; set; }
    }
}