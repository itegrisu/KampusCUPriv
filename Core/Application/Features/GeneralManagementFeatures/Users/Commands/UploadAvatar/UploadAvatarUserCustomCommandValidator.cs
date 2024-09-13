using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatar;

public class UploadAvatarUserCustomCommandValidator : AbstractValidator<UploadAvatarUserCommand>
{
    public UploadAvatarUserCustomCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();

    }
}