using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CreateServiceWithGroup
{
    public class CreatedTransportationServiceWithGroupResponse : BaseResponse, IResponse
    {
        public GetByGidTransportationServiceResponse Obj { get; set; }
    }
}
