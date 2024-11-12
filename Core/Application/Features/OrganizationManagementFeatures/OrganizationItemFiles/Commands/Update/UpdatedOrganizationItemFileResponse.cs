using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Update;

public class UpdatedOrganizationItemFileResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationItemFileResponse Obj { get; set; }
}