using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid
{
    public class GetByGidUserModuleAuthResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public EnumModuleType ModuleType { get; set; }

    }
}