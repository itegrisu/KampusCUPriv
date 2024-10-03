using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.UploadDocumentFile;

public class UploadDocumentDetailValidator : AbstractValidator<UploadDetailDocumentCommand>
{
    public UploadDocumentDetailValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();

    }
}