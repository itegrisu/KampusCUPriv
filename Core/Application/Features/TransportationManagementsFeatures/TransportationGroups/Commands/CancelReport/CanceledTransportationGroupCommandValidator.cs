using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationGroups.Commands.CancelReport
{
    public class CanceledTransportationGroupCommandValidator : AbstractValidator<CancelTransportationGroupCommand>
    {
        public CanceledTransportationGroupCommandValidator()
        {
            RuleFor(c => c.RefNoTransportationGroup).NotNull().NotEmpty();
        }
    }
}
