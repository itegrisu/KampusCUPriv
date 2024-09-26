using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid
{
    public class GetByGidTaskUserResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public Guid GidTaskFK { get; set; }
        public EnumTaskState TaskState { get; set; }
        public string? StatusNote { get; set; }
    }
}