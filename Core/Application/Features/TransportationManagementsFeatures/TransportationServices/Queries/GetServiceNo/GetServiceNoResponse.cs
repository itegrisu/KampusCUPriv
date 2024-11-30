using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetServiceNo
{
    public class GetServiceNoResponse :IResponse
    {
        public string ServiceNo { get; set; }
    }
}
