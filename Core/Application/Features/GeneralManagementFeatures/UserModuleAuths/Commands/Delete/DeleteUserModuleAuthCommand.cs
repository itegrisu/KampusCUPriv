using Application.Features.GeneralManagementFeatures.UserModuleAuths.Constants;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Rules;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Delete;

public class DeleteUserModuleAuthCommand : IRequest<DeletedUserModuleAuthResponse>
{
	public Guid Gid { get; set; }

    public class DeleteUserModuleAuthCommandHandler : IRequestHandler<DeleteUserModuleAuthCommand, DeletedUserModuleAuthResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserModuleAuthReadRepository _userModuleAuthReadRepository;
        private readonly IUserModuleAuthWriteRepository _userModuleAuthWriteRepository;
        private readonly UserModuleAuthBusinessRules _userModuleAuthBusinessRules;

        public DeleteUserModuleAuthCommandHandler(IMapper mapper, IUserModuleAuthReadRepository userModuleAuthReadRepository,
                                         UserModuleAuthBusinessRules userModuleAuthBusinessRules, IUserModuleAuthWriteRepository userModuleAuthWriteRepository)
        {
            _mapper = mapper;
            _userModuleAuthReadRepository = userModuleAuthReadRepository;
            _userModuleAuthBusinessRules = userModuleAuthBusinessRules;
            _userModuleAuthWriteRepository = userModuleAuthWriteRepository;
        }

        public async Task<DeletedUserModuleAuthResponse> Handle(DeleteUserModuleAuthCommand request, CancellationToken cancellationToken)
        {
            X.UserModuleAuth? userModuleAuth = await _userModuleAuthReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _userModuleAuthBusinessRules.UserModuleAuthShouldExistWhenSelected(userModuleAuth);
            userModuleAuth.DataState = Core.Enum.DataState.Deleted;

            _userModuleAuthWriteRepository.Update(userModuleAuth);
            await _userModuleAuthWriteRepository.SaveAsync();

            return new()
            {
                Title = UserModuleAuthsBusinessMessages.ProcessCompleted,
                Message = UserModuleAuthsBusinessMessages.SuccessDeletedUserModuleAuthMessage,
                IsValid = true
            };
        }
    }
}