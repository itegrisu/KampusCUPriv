using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid
{
    public class GetByGidPersonnelWorkingTableResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExitDate { get; set; }

    }
}