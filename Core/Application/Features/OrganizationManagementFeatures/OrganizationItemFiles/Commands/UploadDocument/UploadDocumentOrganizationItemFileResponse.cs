using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.UploadDocument
{
    public class UploadDocumentOrganizationItemFileResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}
