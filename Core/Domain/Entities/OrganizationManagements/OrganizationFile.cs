using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrganizationManagements
{
    public class OrganizationFile : BaseEntity, IHasRowNo
    {

        public Guid GidOrganizationFK { get; set; }
        public Organization OrganizationFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Document { get; set; }
        public string? Description { get; set; }
        public int RowNo { get; set; }


    }
}
