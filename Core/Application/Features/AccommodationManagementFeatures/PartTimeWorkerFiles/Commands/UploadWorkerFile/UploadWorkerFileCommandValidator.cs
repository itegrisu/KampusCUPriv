using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.UploadDocumentFile;
using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.UploadWorkerFile;

public class UploadWorkerFileCommandValidator : AbstractValidator<UploadDetailDocumentCommand>
{
    public UploadWorkerFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();

    }
}