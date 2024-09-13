using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetList;

public class GetListPersonnelWorkingTableListItemDto : IDto
{
    public Guid Gid { get; set; }
public Guid GidPersonelFK { get; set; }
public User UserFK { get; set; }

public DateTime IseBaslamaTarihi { get; set; }
public DateTime IstenCikisTarihi { get; set; }


}