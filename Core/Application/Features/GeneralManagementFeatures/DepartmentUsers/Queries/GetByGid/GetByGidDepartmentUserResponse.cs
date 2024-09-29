using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid
{
    public class GetByGidDepartmentUserResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidDepartmentFK { get; set; }
        public string DepartmentFKName { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
    }
}