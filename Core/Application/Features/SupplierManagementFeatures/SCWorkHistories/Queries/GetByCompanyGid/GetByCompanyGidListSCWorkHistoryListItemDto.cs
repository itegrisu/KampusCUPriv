using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SupplierManagementFeatures.SCWorkHistories.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCWorkHistoryListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public string Title { get; set; }
        public string? Detail { get; set; }
        public DateTime WorkDate { get; set; }
        public string? WorkFile { get; set; }
    }
}
