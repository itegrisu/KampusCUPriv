using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Commands.Report
{
    public class ReportTransportationPersonnelCommandValidator : AbstractValidator<ReportTransportationPersonnelCommand>
    {
        public ReportTransportationPersonnelCommandValidator()
        {
        }
    }
}
