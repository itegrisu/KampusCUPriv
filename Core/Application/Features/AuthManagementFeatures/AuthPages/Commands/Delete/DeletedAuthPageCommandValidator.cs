using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Delete;

public class DeleteAuthPageCommandValidator : AbstractValidator<DeleteAuthPageCommand>
{
    public DeleteAuthPageCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}