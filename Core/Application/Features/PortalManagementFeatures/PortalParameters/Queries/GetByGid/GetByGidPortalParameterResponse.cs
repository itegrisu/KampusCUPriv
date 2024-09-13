using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid
{
    public class GetByGidPortalParameterResponse : IResponse
    {
        public Guid Gid { get; set; }

        public string Name { get; set; }
        public EnumParameterValueType ParameterValueType { get; set; }
        public string? StringValue { get; set; }
        public int IntegerValue { get; set; }
        public decimal DecimalValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public string? Description { get; set; }

    }
}