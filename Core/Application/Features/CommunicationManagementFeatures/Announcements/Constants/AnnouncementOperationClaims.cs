namespace Application.Features.CommunicationFeatures.Announcements.Constants;

public static class AnnouncementCustomsOperationClaims
{
    private const string _section = "AnnouncementCustoms";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
}