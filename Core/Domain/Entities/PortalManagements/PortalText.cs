using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PortalManagements
{
    public class PortalText : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public string? Description { get; set; }
        public bool IsRichTextBox { get; set; }
        public string? ContentRich { get; set; }

    }
}
