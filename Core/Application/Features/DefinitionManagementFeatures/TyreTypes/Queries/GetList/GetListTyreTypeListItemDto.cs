using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetList;

public class GetListTyreTypeListItemDto : IDto
{
    public Guid Gid { get; set; }

    public string Title { get; set; }
    public EnumTyreTypeName TyreTypeName { get; set; }
    public string? Size { get; set; }


}