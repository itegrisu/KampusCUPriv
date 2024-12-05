using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UpdateServiceWithGroup
{
    public class UpdateServiceWithGroupTransportationServiceCommandValidator : AbstractValidator<UpdateServiceWithGroupTransportationServiceCommand>
    {
        public UpdateServiceWithGroupTransportationServiceCommandValidator()
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

            RuleFor(c => c.GroupGid).NotNull().NotEmpty();
            RuleFor(c => c.GidTransportationServiceFK).NotNull().NotEmpty();
            RuleFor(c => c.GidStartCountryFK).NotNull().NotEmpty();
            RuleFor(c => c.GidStartCityFK).NotNull().NotEmpty();
            RuleFor(c => c.GidStartDistrictFK).NotNull().NotEmpty();
            RuleFor(c => c.GidEndCountryFK).NotNull().NotEmpty();
            RuleFor(c => c.GidEndCityFK).NotNull().NotEmpty();
            RuleFor(c => c.GidEndDistrictFK).NotNull().NotEmpty();
            RuleFor(c => c.GroupName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.TransportationFee).NotNull().NotEmpty();
            RuleFor(c => c.StartPlace).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(c => c.EndPlace).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(c => c.GroupDescription).MaximumLength(250);
        }
    }
}
