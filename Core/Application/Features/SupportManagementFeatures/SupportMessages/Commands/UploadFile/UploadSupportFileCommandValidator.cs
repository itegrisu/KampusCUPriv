using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.UploadFile;

public class UploadSupportFileCommandValidator : AbstractValidator<UploadSupportFileCommand>
{
    public UploadSupportFileCommandValidator()
    {
        RuleFor(c => c.SupportRequestGid).NotEmpty();
        RuleFor(c => c.FileName).NotNull();

    }
}