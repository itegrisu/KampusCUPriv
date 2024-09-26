using Application.Features.TaskManagementFeatures.TaskGroupUsers.Constants;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Rules;
using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Delete;

public class DeleteTaskGroupUserCommand : IRequest<DeletedTaskGroupUserResponse>
{
    public Guid Gid { get; set; }

    public class DeleteTaskGroupUserCommandHandler : IRequestHandler<DeleteTaskGroupUserCommand, DeletedTaskGroupUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskGroupUserReadRepository _taskGroupUserReadRepository;
        private readonly ITaskGroupUserWriteRepository _taskGroupUserWriteRepository;
        private readonly TaskGroupUserBusinessRules _taskGroupUserBusinessRules;

        public DeleteTaskGroupUserCommandHandler(IMapper mapper, ITaskGroupUserReadRepository taskGroupUserReadRepository,
                                         TaskGroupUserBusinessRules taskGroupUserBusinessRules, ITaskGroupUserWriteRepository taskGroupUserWriteRepository)
        {
            _mapper = mapper;
            _taskGroupUserReadRepository = taskGroupUserReadRepository;
            _taskGroupUserBusinessRules = taskGroupUserBusinessRules;
            _taskGroupUserWriteRepository = taskGroupUserWriteRepository;
        }

        public async Task<DeletedTaskGroupUserResponse> Handle(DeleteTaskGroupUserCommand request, CancellationToken cancellationToken)
        {
            await _taskGroupUserBusinessRules.TaskGroupUserShouldExistWhenSelected(request.Gid);

            X.TaskGroupUser? taskGroupUser = await _taskGroupUserReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            taskGroupUser.DataState = Core.Enum.DataState.Deleted;

            _taskGroupUserWriteRepository.Update(taskGroupUser);
            await _taskGroupUserWriteRepository.SaveAsync();

            return new()
            {
                Title = TaskGroupUsersBusinessMessages.ProcessCompleted,
                Message = TaskGroupUsersBusinessMessages.SuccessDeletedTaskGroupUserMessage,
                IsValid = true
            };
        }
    }
}