using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.ReportPersonnel
{
    public class ReportedTransportationPassengerResponse : BaseResponse, IResponse
    {
        public GetByGidTransportationPassengerResponse Obj { get; set; }
    }
}
