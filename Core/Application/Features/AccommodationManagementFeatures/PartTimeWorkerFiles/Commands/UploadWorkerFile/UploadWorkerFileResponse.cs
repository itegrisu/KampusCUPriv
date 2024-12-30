using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.UploadDocumentFile
{
    public class UploadWorkerFileResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}
