using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.ReportPersonnel
{
    public class ReportTransportationPassengerCommandValidator : AbstractValidator<ReportTransportationPassengerCommand>
    {
        public ReportTransportationPassengerCommandValidator()
        {
            RuleFor(c => c.Gid).NotNull().NotEmpty();

            RuleFor(c => c.Country).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(c => c.IdentityNo).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(c => c.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(c => c.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(c => c.Gender).NotNull().NotEmpty();
            RuleFor(c => c.Phone).MaximumLength(20);
            RuleFor(c => c.PassengerStatus).NotNull().NotEmpty();
            RuleFor(c => c.RefNoTransportationPassenger).MaximumLength(50);
        }
    }
}
