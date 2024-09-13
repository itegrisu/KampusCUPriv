using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid
{
    public class GetByGidRoomTypeResponse : IResponse
    {
        public Guid Gid { get; set; }

public string OdaTuru { get; set; }
public string OdaKodu { get; set; }
public int KisiSayisi { get; set; }
public string? Aciklama { get; set; }

    }
}