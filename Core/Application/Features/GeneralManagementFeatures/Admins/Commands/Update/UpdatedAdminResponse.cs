using Application.Features.Base;
using Application.Features.GeneralFeatures.Admins.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralFeatures.Admins.Commands.Update;

public class UpdatedAdminResponse : BaseResponse, IResponse
{
    public GetByGidAdminResponse Obj { get; set; }
}