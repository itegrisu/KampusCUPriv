using MediatR;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.UploadFile
{
    public class UploadOfferFileCommand : IRequest<UploadOfferFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
