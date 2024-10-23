using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelWorkingTableListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExitDate { get; set; }
    }
}
