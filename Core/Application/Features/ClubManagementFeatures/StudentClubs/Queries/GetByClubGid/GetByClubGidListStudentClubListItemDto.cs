using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClubManagementFeatures.StudentClubs.Queries.GetByClubGid
{
    public class GetByClubGidListStudentClubListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKName { get; set; }
        public string UserFKLastName { get; set; }
        public string UserFKClassFKName { get; set; }
        public string UserFKDepartmentFKName { get; set; }
        public Guid GidClubFK { get; set; }
        public string ClubFKName { get; set; }
        public string ClubFKLogo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
