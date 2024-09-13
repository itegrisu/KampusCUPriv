using Core.Application.Dtos;
using Core.Enum;


namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetList;

public class GetListUserListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidAcademicTitleFK { get; set; }
    public Guid GidInstituteFK { get; set; }
    public string AcademicTitleFKTitle { get; set; }
    public string UniversityFKUniversityName { get; set; }
    public string? Avatar { get; set; }
    public string? TitleOther { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string IdNumber { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
    public string? Company { get; set; }
    public bool IsSystemAdmin { get; set; }
    public bool IsLoginStatus { get; set; }
    public DataState DataState { get; set; }
    public string FullName { get; set; }
    public string FullTitle { get; set; }
    public string FullInstitute { get; set; }
    public string FullPhone { get; set; }
    public bool IsRequestMode { get; set; }
    public bool IsExclusiveMode { get; set; }
    public bool IsEngineer { get; set; }
}