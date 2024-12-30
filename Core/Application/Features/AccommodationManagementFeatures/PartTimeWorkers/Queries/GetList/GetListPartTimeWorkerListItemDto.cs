using Core.Application.Dtos;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetList;

public class GetListPartTimeWorkerListItemDto : IDto
{
    public Guid Gid { get; set; }

    public string IdentityNo { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }
    public bool IsLoginStatus { get; set; }
    public string Gsm { get; set; }
    public DateTime BirthDate { get; set; }

    public string Languages { get; set; }

}