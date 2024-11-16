using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid
{
    public class GetByGidTyreTypeResponse : IResponse
    {
        public Guid Gid { get; set; }

        public string Title { get; set; }
        public EnumTyreTypeName TyreTypeName { get; set; }
        public string? Size { get; set; }

    }
}