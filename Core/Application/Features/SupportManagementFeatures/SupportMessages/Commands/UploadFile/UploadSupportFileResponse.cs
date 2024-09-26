using Application.Features.Base;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.UploadFile
{
    public class UploadSupportFileResponse : BaseResponse, IResponse
    {
        public GetByGidSupportMessageResponse Obj { get; set; }

    }
}
