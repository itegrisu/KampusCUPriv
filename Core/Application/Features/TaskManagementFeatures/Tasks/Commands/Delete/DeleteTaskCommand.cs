using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Features.TaskManagementFeatures.Tasks.Rules;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using AutoMapper;
using MediatR;
using T = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Delete;

public class DeleteTaskCommand : IRequest<DeletedTaskResponse>
{
    public Guid Gid { get; set; }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeletedTaskResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly ITaskWriteRepository _taskWriteRepository;
        private readonly TaskBusinessRules _taskBusinessRules;

        public DeleteTaskCommandHandler(IMapper mapper, ITaskReadRepository taskReadRepository,
                                         TaskBusinessRules taskBusinessRules, ITaskWriteRepository taskWriteRepository)
        {
            _mapper = mapper;
            _taskReadRepository = taskReadRepository;
            _taskBusinessRules = taskBusinessRules;
            _taskWriteRepository = taskWriteRepository;
        }

        public async Task<DeletedTaskResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskBusinessRules.TaskShouldExistWhenSelected(request.Gid);

            T.Task? task = await _taskReadRepository.GetAsync(predicate: t => t.Gid == request.Gid, cancellationToken: cancellationToken);

            task.DataState = Core.Enum.DataState.Deleted;
            _taskWriteRepository.Update(task);
            await _taskWriteRepository.SaveAsync();

            return new()
            {
                Title = TasksBusinessMessages.ProcessCompleted,
                Message = TasksBusinessMessages.SuccessDeletedTaskMessage,
                IsValid = true
            };
        }
    }
}