using Core.Application.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.Transportations.Queries.GetTransportationNo
{
    public class GetTransportationNoResponse : IResponse
    {
        public string TransportationNo { get; set; }
    }
}
