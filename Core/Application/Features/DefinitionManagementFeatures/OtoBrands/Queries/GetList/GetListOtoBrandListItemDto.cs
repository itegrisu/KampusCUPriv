using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetList;

public class GetListOtoBrandListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string AracMarkaAdi { get; set; }


}