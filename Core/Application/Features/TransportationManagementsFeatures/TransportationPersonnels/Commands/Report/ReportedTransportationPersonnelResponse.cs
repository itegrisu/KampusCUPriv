using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Commands.Report
{
    public class ReportedTransportationPersonnelResponse : BaseResponse, IResponse
    {
        public GetByGidTransportationPersonnelResponse Obj { get; set; }
    }
}
