using Application.Features.TaskManagementFeatures.TaskManagers.Constants;
using Application.Features.TaskManagementFeatures.TaskManagers.Rules;
using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Commands.Delete;

public class DeleteTaskManagerCommand : IRequest<DeletedTaskManagerResponse>
{
    public Guid Gid { get; set; }

    public class DeleteTaskManagerCommandHandler : IRequestHandler<DeleteTaskManagerCommand, DeletedTaskManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskManagerReadRepository _taskManagerReadRepository;
        private readonly ITaskManagerWriteRepository _taskManagerWriteRepository;
        private readonly TaskManagerBusinessRules _taskManagerBusinessRules;

        public DeleteTaskManagerCommandHandler(IMapper mapper, ITaskManagerReadRepository taskManagerReadRepository,
                                         TaskManagerBusinessRules taskManagerBusinessRules, ITaskManagerWriteRepository taskManagerWriteRepository)
        {
            _mapper = mapper;
            _taskManagerReadRepository = taskManagerReadRepository;
            _taskManagerBusinessRules = taskManagerBusinessRules;
            _taskManagerWriteRepository = taskManagerWriteRepository;
        }

        public async Task<DeletedTaskManagerResponse> Handle(DeleteTaskManagerCommand request, CancellationToken cancellationToken)
        {
            await _taskManagerBusinessRules.TaskManagerShouldExistWhenSelected(request.Gid);

            X.TaskManager? taskManager = await _taskManagerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            taskManager.DataState = Core.Enum.DataState.Deleted;

            _taskManagerWriteRepository.Update(taskManager);
            await _taskManagerWriteRepository.SaveAsync();

            return new()
            {
                Title = TaskManagersBusinessMessages.ProcessCompleted,
                Message = TaskManagersBusinessMessages.SuccessDeletedTaskManagerMessage,
                IsValid = true
            };
        }
    }
}