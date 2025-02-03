using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClubManagementFeatures.Clubs.Queries.GetByCount
{
    public class GetByCountListClubListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid? GidManagerFK { get; set; }
        public string UserFKName { get; set; }
        public Guid GidCategoryFK { get; set; }
        public string CategoryFKName { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
    }
}
