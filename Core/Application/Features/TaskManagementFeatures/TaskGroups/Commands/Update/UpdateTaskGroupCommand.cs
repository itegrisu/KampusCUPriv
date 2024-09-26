using Application.Features.TaskManagementFeatures.TaskGroups.Constants;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroups.Rules;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Update;

public class UpdateTaskGroupCommand : IRequest<UpdatedTaskGroupResponse>
{
    public Guid Gid { get; set; }
    public string GroupName { get; set; }

    public class UpdateTaskGroupCommandHandler : IRequestHandler<UpdateTaskGroupCommand, UpdatedTaskGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskGroupWriteRepository _taskGroupWriteRepository;
        private readonly ITaskGroupReadRepository _taskGroupReadRepository;
        private readonly TaskGroupBusinessRules _taskGroupBusinessRules;

        public UpdateTaskGroupCommandHandler(IMapper mapper, ITaskGroupWriteRepository taskGroupWriteRepository,
                                         TaskGroupBusinessRules taskGroupBusinessRules, ITaskGroupReadRepository taskGroupReadRepository)
        {
            _mapper = mapper;
            _taskGroupWriteRepository = taskGroupWriteRepository;
            _taskGroupBusinessRules = taskGroupBusinessRules;
            _taskGroupReadRepository = taskGroupReadRepository;
        }

        public async Task<UpdatedTaskGroupResponse> Handle(UpdateTaskGroupCommand request, CancellationToken cancellationToken)
        {
            await _taskGroupBusinessRules.TaskGroupShouldExistWhenSelected(request.Gid);

            X.TaskGroup? taskGroup = await _taskGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid,
                include: x => x.Include(i => i.TaskGroupUsers),
                cancellationToken: cancellationToken); //, //TODO);
            taskGroup = _mapper.Map(request, taskGroup);

            _taskGroupWriteRepository.Update(taskGroup!);
            await _taskGroupWriteRepository.SaveAsync();
            GetByGidTaskGroupResponse obj = _mapper.Map<GetByGidTaskGroupResponse>(taskGroup);

            return new()
            {
                Title = TaskGroupsBusinessMessages.ProcessCompleted,
                Message = TaskGroupsBusinessMessages.SuccessUpdatedTaskGroupMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}