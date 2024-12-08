using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.CancelReport
{
    public class CanceledTransportationPassengerCommandValidator : AbstractValidator<CancelTransportationPassengerCommand>
    {
        public CanceledTransportationPassengerCommandValidator()
        {
            RuleFor(x => x.Gid).NotEmpty().NotNull();
        }
    }
}
