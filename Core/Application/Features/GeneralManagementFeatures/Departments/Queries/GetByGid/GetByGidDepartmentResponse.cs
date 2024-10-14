using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid
{
    public class GetByGidDepartmentResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidMainAdminFK { get; set; }
        public string MainAdminFKFullName { get; set; }
        public Guid? GidCoAdminFK { get; set; }
        public string CoAdminFKFullName { get; set; }
        public string Name { get; set; }
        public string? Details { get; set; }


    }
}