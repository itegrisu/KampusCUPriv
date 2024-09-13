using Application.Features.LogManagementFeatures.LogFailedLogins.Constants;
using Application.Features.LogManagementFeatures.LogFailedLogins.Rules;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Delete;

public class DeleteLogFailedLoginCommand : IRequest<DeletedLogFailedLoginResponse>
{
	public Guid Gid { get; set; }

    public class DeleteLogFailedLoginCommandHandler : IRequestHandler<DeleteLogFailedLoginCommand, DeletedLogFailedLoginResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogFailedLoginReadRepository _logFailedLoginReadRepository;
        private readonly ILogFailedLoginWriteRepository _logFailedLoginWriteRepository;
        private readonly LogFailedLoginBusinessRules _logFailedLoginBusinessRules;

        public DeleteLogFailedLoginCommandHandler(IMapper mapper, ILogFailedLoginReadRepository logFailedLoginReadRepository,
                                         LogFailedLoginBusinessRules logFailedLoginBusinessRules, ILogFailedLoginWriteRepository logFailedLoginWriteRepository)
        {
            _mapper = mapper;
            _logFailedLoginReadRepository = logFailedLoginReadRepository;
            _logFailedLoginBusinessRules = logFailedLoginBusinessRules;
            _logFailedLoginWriteRepository = logFailedLoginWriteRepository;
        }

        public async Task<DeletedLogFailedLoginResponse> Handle(DeleteLogFailedLoginCommand request, CancellationToken cancellationToken)
        {
            X.LogFailedLogin? logFailedLogin = await _logFailedLoginReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _logFailedLoginBusinessRules.LogFailedLoginShouldExistWhenSelected(logFailedLogin);
            logFailedLogin.DataState = Core.Enum.DataState.Deleted;

            _logFailedLoginWriteRepository.Update(logFailedLogin);
            await _logFailedLoginWriteRepository.SaveAsync();

            return new()
            {
                Title = LogFailedLoginsBusinessMessages.ProcessCompleted,
                Message = LogFailedLoginsBusinessMessages.SuccessDeletedLogFailedLoginMessage,
                IsValid = true
            };
        }
    }
}