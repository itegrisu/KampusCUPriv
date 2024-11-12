using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.UploadDocument
{
    public class UploadDocumentOrganizationItemFileCommandValidator : AbstractValidator<UploadDocumentOrganizationItemFileCommand>
    {
        public UploadDocumentOrganizationItemFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
