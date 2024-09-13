using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetSystemAdmin
{
    public class GetSystemAdminUserListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public bool IsSystemAdmin { get; set; }
        public bool IsLoginStatus { get; set; }
        public DataState DataState { get; set; }
    }
}