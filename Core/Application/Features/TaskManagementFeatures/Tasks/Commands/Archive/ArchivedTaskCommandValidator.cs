using Application.Features.TaskManagementFeatures.Tasks.Commands.Delete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Archive
{
    public class ArchivedTaskCommandValidator : AbstractValidator<ArchivedTaskCommand>
    {
        public ArchivedTaskCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
