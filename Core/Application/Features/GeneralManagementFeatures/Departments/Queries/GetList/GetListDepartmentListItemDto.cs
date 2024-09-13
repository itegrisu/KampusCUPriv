using Core.Application.Dtos;

namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetList;

public class GetListDepartmentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidAsilYoneticiFK { get; set; }
    public string AsilYoneticiFKTamAd { get; set; }
    public Guid? GidYedekYoneticiFK { get; set; }
    public string YedekYoneticiTamAd { get; set; }
    public string DepartmanAdi { get; set; }
    public string? Detay { get; set; }


}