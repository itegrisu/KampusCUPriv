using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.ProjectManagementFeatures.ProjectFiles.Commands
{
    public class UploadStockMovementResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}
