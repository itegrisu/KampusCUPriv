using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.GeneralManagements
{
    public class UserShortCut : BaseEntity, IHasRowNo
    {

        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public string PageName { get; set; } = string.Empty;
        public string PageUrl { get; set; } = string.Empty;
        public int RowNo { get; set; }
    }
}
