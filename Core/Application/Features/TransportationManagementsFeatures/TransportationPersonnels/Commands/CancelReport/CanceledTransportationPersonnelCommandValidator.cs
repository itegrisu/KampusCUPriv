using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Commands.CancelReport
{
    public class CanceledTransportationPersonnelCommandValidator : AbstractValidator<CancelTransportationPersonnelCommand>
    {
        public CanceledTransportationPersonnelCommandValidator()
        {
            RuleFor(c => c.Gid).NotNull().NotEmpty();
        }
    }
}
