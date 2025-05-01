using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClubManagementFeatures.StudentClubs.Queries.GetByUserGid
{
    public class GetByUserGidListStudentClubListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKName { get; set; }
        public Guid GidClubFK { get; set; }
        public string ClubFKName { get; set; }
        public string ClubFKLogo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
