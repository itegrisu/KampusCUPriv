using Application.Features.TaskManagementFeatures.TaskGroups.Constants;
using Application.Features.TaskManagementFeatures.TaskGroups.Rules;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Delete;

public class DeleteTaskGroupCommand : IRequest<DeletedTaskGroupResponse>
{
    public Guid Gid { get; set; }

    public class DeleteTaskGroupCommandHandler : IRequestHandler<DeleteTaskGroupCommand, DeletedTaskGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskGroupReadRepository _taskGroupReadRepository;
        private readonly ITaskGroupWriteRepository _taskGroupWriteRepository;
        private readonly TaskGroupBusinessRules _taskGroupBusinessRules;

        public DeleteTaskGroupCommandHandler(IMapper mapper, ITaskGroupReadRepository taskGroupReadRepository,
                                         TaskGroupBusinessRules taskGroupBusinessRules, ITaskGroupWriteRepository taskGroupWriteRepository)
        {
            _mapper = mapper;
            _taskGroupReadRepository = taskGroupReadRepository;
            _taskGroupBusinessRules = taskGroupBusinessRules;
            _taskGroupWriteRepository = taskGroupWriteRepository;
        }

        public async Task<DeletedTaskGroupResponse> Handle(DeleteTaskGroupCommand request, CancellationToken cancellationToken)
        {
            await _taskGroupBusinessRules.TaskGroupShouldExistWhenSelected(request.Gid);

            X.TaskGroup? taskGroup = await _taskGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid,
                cancellationToken: cancellationToken, include: x => x.Include(x => x.TaskGroupUsers));

            taskGroup.DataState = Core.Enum.DataState.Deleted;

            _taskGroupWriteRepository.Update(taskGroup);
            await _taskGroupWriteRepository.SaveAsync();

            return new()
            {
                Title = TaskGroupsBusinessMessages.ProcessCompleted,
                Message = TaskGroupsBusinessMessages.SuccessDeletedTaskGroupMessage,
                IsValid = true
            };
        }
    }
}