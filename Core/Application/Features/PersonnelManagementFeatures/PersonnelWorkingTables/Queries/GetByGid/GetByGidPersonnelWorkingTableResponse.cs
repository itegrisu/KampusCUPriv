using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid
{
    public class GetByGidPersonnelWorkingTableResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonelFK { get; set; }
        public string UserFKTamAd { get; set; }
        public DateTime IseBaslamaTarihi { get; set; }
        public DateTime IstenCikisTarihi { get; set; }

    }
}