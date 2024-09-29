using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetList;

public class GetListPersonnelDocumentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonnelFK { get; set; }
    public string UserFKFullName { get; set; }
    public Guid GidDocumentType { get; set; }
    public string DocumentTypeFKName { get; set; }

    public string Name { get; set; }
    public DateTime ValidityDate { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }


}