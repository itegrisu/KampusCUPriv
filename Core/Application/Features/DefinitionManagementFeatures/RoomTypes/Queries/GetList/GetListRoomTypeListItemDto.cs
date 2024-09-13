using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetList;

public class GetListRoomTypeListItemDto : IDto
{
    public Guid Gid { get; set; }

public string OdaTuru { get; set; }
public string OdaKodu { get; set; }
public int KisiSayisi { get; set; }
public string? Aciklama { get; set; }


}