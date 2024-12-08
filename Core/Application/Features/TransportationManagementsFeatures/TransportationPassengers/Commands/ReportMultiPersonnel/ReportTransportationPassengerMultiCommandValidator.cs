using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.ReportMultiPersonnel
{
    public class ReportTransportationPassengerMultiCommandValidator : AbstractValidator<ReportTransportationPassengerMultiCommand>
    {
        public ReportTransportationPassengerMultiCommandValidator()
        {
        }
    }
}
