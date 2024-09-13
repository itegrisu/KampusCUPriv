using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid
{
    public class GetByGidDepartmentUserResponse : IResponse
    {
        public Guid Gid { get; set; }
public Guid GidDepartmanFK { get; set; }
public Department DepartmentFK { get; set; }
public Guid GidPersonelFK { get; set; }
public User UserFK { get; set; }


    }
}