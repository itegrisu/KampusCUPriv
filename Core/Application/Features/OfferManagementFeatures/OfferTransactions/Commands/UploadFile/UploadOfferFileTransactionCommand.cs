using MediatR;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.UploadFile
{
    public class UploadOfferFileTransactionCommand : IRequest<UploadOfferTransactionFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
