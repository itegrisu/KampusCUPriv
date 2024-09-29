using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetList;

public class GetListPersonnelWorkingTableListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonnelFK { get; set; }
    public string UserFKFullName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExitDate { get; set; }


}