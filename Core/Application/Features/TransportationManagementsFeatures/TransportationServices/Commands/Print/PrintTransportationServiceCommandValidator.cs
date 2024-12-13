using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.Print
{
    public class PrintTransportationServiceCommandValidator : AbstractValidator<PrintTransportationServiceCommand>
    {
        public PrintTransportationServiceCommandValidator()
        {
            RuleFor(c => c.Gid).NotNull().NotEmpty();
        }
    }
}
