using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CancelReport
{
    public class CancaledTransportationServiceCommandValidator : AbstractValidator<CancelTransportationServiceCommand>
    {
        public CancaledTransportationServiceCommandValidator()
        {
            RuleFor(c => c.refNoTransportation).NotNull().NotEmpty();
        }
    }
}
