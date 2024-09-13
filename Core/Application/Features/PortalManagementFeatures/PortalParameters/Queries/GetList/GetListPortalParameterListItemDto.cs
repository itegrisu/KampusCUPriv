using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetList;

public class GetListPortalParameterListItemDto : IDto
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