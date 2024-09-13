namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Constants;

public static class PersonnelDocumentCustomsOperationClaims
{
    private const string _section = "PersonnelDocumentCustoms";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
}