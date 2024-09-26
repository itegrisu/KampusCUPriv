using Application.Features.TaskManagementFeatures.TaskManagers.Constants;
using Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskManagers.Rules;
using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Commands.Update;

public class UpdateTaskManagerCommand : IRequest<UpdatedTaskManagerResponse>
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }

    public class UpdateTaskManagerCommandHandler : IRequestHandler<UpdateTaskManagerCommand, UpdatedTaskManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskManagerWriteRepository _taskManagerWriteRepository;
        private readonly ITaskManagerReadRepository _taskManagerReadRepository;
        private readonly TaskManagerBusinessRules _taskManagerBusinessRules;

        public UpdateTaskManagerCommandHandler(IMapper mapper, ITaskManagerWriteRepository taskManagerWriteRepository,
                                         TaskManagerBusinessRules taskManagerBusinessRules, ITaskManagerReadRepository taskManagerReadRepository)
        {
            _mapper = mapper;
            _taskManagerWriteRepository = taskManagerWriteRepository;
            _taskManagerBusinessRules = taskManagerBusinessRules;
            _taskManagerReadRepository = taskManagerReadRepository;
        }

        public async Task<UpdatedTaskManagerResponse> Handle(UpdateTaskManagerCommand request, CancellationToken cancellationToken)
        {
            await _taskManagerBusinessRules.TaskManagerShouldExistWhenSelected(request.Gid);
            await _taskManagerBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            await _taskManagerBusinessRules.IsTaskManagerExist(request.GidUserFK);

            X.TaskManager? taskManager = await _taskManagerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken); //, //TODO);
            taskManager = _mapper.Map(request, taskManager);

            _taskManagerWriteRepository.Update(taskManager!);
            await _taskManagerWriteRepository.SaveAsync();
            GetByGidTaskManagerResponse obj = _mapper.Map<GetByGidTaskManagerResponse>(taskManager);

            return new()
            {
                Title = TaskManagersBusinessMessages.ProcessCompleted,
                Message = TaskManagersBusinessMessages.SuccessUpdatedTaskManagerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}