using Application.Features.LogManagementFeatures.LogSuccessedLogins.Constants;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Rules;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Delete;

public class DeleteLogSuccessedLoginCommand : IRequest<DeletedLogSuccessedLoginResponse>
{
	public Guid Gid { get; set; }

    public class DeleteLogSuccessedLoginCommandHandler : IRequestHandler<DeleteLogSuccessedLoginCommand, DeletedLogSuccessedLoginResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
        private readonly ILogSuccessedLoginWriteRepository _logSuccessedLoginWriteRepository;
        private readonly LogSuccessedLoginBusinessRules _logSuccessedLoginBusinessRules;

        public DeleteLogSuccessedLoginCommandHandler(IMapper mapper, ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository,
                                         LogSuccessedLoginBusinessRules logSuccessedLoginBusinessRules, ILogSuccessedLoginWriteRepository logSuccessedLoginWriteRepository)
        {
            _mapper = mapper;
            _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
            _logSuccessedLoginBusinessRules = logSuccessedLoginBusinessRules;
            _logSuccessedLoginWriteRepository = logSuccessedLoginWriteRepository;
        }

        public async Task<DeletedLogSuccessedLoginResponse> Handle(DeleteLogSuccessedLoginCommand request, CancellationToken cancellationToken)
        {
            X.LogSuccessedLogin? logSuccessedLogin = await _logSuccessedLoginReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _logSuccessedLoginBusinessRules.LogSuccessedLoginShouldExistWhenSelected(logSuccessedLogin);
            logSuccessedLogin.DataState = Core.Enum.DataState.Deleted;

            _logSuccessedLoginWriteRepository.Update(logSuccessedLogin);
            await _logSuccessedLoginWriteRepository.SaveAsync();

            return new()
            {
                Title = LogSuccessedLoginsBusinessMessages.ProcessCompleted,
                Message = LogSuccessedLoginsBusinessMessages.SuccessDeletedLogSuccessedLoginMessage,
                IsValid = true
            };
        }
    }
}