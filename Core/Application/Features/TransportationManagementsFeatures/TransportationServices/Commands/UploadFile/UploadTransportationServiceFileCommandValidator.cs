using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UploadFile
{
    public class UploadTransportationServiceFileCommandValidator : AbstractValidator<UploadTransportationServiceFileCommand>
    {
        public UploadTransportationServiceFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
