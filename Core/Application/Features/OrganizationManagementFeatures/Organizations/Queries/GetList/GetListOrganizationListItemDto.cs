using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetList;

public class GetListOrganizationListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidCustomerFK { get; set; }
    public string SCCompanyFKCompanyName { get; set; }
    public Guid GidResponsibleUserFK { get; set; }
    public string ResponsibleUserFKFullName { get; set; }
    public Guid GidOrganizationTypeFK { get; set; }
    public string OrganizationTypeFKName { get; set; }
    public string OrganizationName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EnumOrganizationStatus OrganizationStatus { get; set; }
    public string? Description { get; set; }


}