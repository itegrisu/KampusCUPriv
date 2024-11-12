using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Update;

public class UpdatedOrganizationItemMessageResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationItemMessageResponse Obj { get; set; }
}