using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PortalManagements
{
    public class PortalParameter : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public EnumParameterValueType ParameterValueType { get; set; }
        public string? StringValue { get; set; }
        public int? IntegerValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public string? Description { get; set; }

    }
}
