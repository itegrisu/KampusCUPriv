using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationGroups.Commands.Report
{
    public class ReportedTransportationGroupResponse : BaseResponse, IResponse
    {
        public GetByGidTransportationGroupResponse Obj { get; set; }
    }
}
