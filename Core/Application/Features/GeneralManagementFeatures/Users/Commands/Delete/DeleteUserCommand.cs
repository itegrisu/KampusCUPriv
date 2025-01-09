using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;

namespace Application.Features.GeneralFeatures.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<DeletedUserResponse>
{
	public Guid Gid { get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository,
                                         UserBusinessRules userBusinessRules, IUserWriteRepository userWriteRepository)
        {
            _mapper = mapper;
            _userReadRepository = userReadRepository;
            _userBusinessRules = userBusinessRules;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldExistWhenSelected(user);
            user.DataState = Core.Enum.DataState.Deleted;

            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();

            return new()
            {
                Title = UsersBusinessMessages.ProcessCompleted,
                Message = UsersBusinessMessages.SuccessDeletedUserMessage,
                IsValid = true
            };
        }
    }
}