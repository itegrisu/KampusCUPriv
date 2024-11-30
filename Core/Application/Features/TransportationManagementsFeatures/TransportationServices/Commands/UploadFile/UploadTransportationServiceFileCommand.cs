using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UploadFile
{
    public class UploadTransportationServiceFileCommand : IRequest<UploadTransportationServiceFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
