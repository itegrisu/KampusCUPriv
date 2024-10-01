namespace Application.Features.OfferManagementFeatures.OfferTransactions.Constants;

public static class OfferTransactionCustomsOperationClaims
{
    private const string _section = "OfferTransactionCustoms";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
}