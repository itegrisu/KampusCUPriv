using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Update;

public class UpdatedOrganizationFileResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationFileResponse Obj { get; set; }
}