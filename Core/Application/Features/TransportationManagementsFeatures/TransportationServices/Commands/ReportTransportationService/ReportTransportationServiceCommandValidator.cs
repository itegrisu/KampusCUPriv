using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.ReportTransportationService
{
    public class ReportTransportationServiceCommandValidator : AbstractValidator<ReportTransportationServiceCommand>
    {
        public ReportTransportationServiceCommandValidator()
        {
            RuleFor(c => c.Gid).NotNull().NotEmpty();
            RuleFor(c => c.GidTransportationFK).NotNull().NotEmpty();
            RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
            RuleFor(c => c.ServiceNo).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(c => c.StartDate).NotNull().NotEmpty();
            RuleFor(c => c.EndDate).NotNull().NotEmpty();
            RuleFor(c => c.VehiclePhone).MaximumLength(20);
            RuleFor(c => c.TransportationServiceStatus).NotNull().NotEmpty();
            RuleFor(c => c.TransportationFile).MaximumLength(150);
            RuleFor(c => c.Description).MaximumLength(250);
            RuleFor(c => c.RefNoTransportation).MaximumLength(100);
        }
    }
}
