using Application.Features.TaskManagementFeatures.TaskUsers.Constants;
using Application.Features.TaskManagementFeatures.TaskUsers.Rules;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Delete;

public class DeleteTaskUserCommand : IRequest<DeletedTaskUserResponse>
{
    public Guid Gid { get; set; }

    public class DeleteTaskUserCommandHandler : IRequestHandler<DeleteTaskUserCommand, DeletedTaskUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskUserReadRepository _taskUserReadRepository;
        private readonly ITaskUserWriteRepository _taskUserWriteRepository;
        private readonly TaskUserBusinessRules _taskUserBusinessRules;

        public DeleteTaskUserCommandHandler(IMapper mapper, ITaskUserReadRepository taskUserReadRepository,
                                         TaskUserBusinessRules taskUserBusinessRules, ITaskUserWriteRepository taskUserWriteRepository)
        {
            _mapper = mapper;
            _taskUserReadRepository = taskUserReadRepository;
            _taskUserBusinessRules = taskUserBusinessRules;
            _taskUserWriteRepository = taskUserWriteRepository;
        }

        public async Task<DeletedTaskUserResponse> Handle(DeleteTaskUserCommand request, CancellationToken cancellationToken)
        {
            await _taskUserBusinessRules.TaskUserShouldExistWhenSelected(request.Gid);

            X.TaskUser? taskUser = await _taskUserReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            taskUser.DataState = Core.Enum.DataState.Deleted;

            _taskUserWriteRepository.Update(taskUser);
            await _taskUserWriteRepository.SaveAsync();

            return new()
            {
                Title = TaskUsersBusinessMessages.ProcessCompleted,
                Message = TaskUsersBusinessMessages.SuccessDeletedTaskUserMessage,
                IsValid = true
            };
        }
    }
}