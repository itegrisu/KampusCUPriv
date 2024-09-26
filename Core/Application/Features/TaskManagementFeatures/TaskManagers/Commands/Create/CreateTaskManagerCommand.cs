using Application.Features.TaskManagementFeatures.TaskManagers.Constants;
using Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskManagers.Rules;
using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using AutoMapper;
using MediatR;
using Domain.Entities.TaskManagements;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Commands.Create;

public class CreateTaskManagerCommand : IRequest<CreatedTaskManagerResponse>
{
    public Guid UserGid { get; set; }

    public class CreateTaskManagerCommandHandler : IRequestHandler<CreateTaskManagerCommand, CreatedTaskManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskManagerWriteRepository _taskManagerWriteRepository;
        private readonly ITaskManagerReadRepository _taskManagerReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly TaskManagerBusinessRules _taskManagerBusinessRules;

        public CreateTaskManagerCommandHandler(IMapper mapper, ITaskManagerWriteRepository taskManagerWriteRepository,
                                         TaskManagerBusinessRules taskManagerBusinessRules, ITaskManagerReadRepository taskManagerReadRepository, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _taskManagerWriteRepository = taskManagerWriteRepository;
            _taskManagerBusinessRules = taskManagerBusinessRules;
            _taskManagerReadRepository = taskManagerReadRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<CreatedTaskManagerResponse> Handle(CreateTaskManagerCommand request, CancellationToken cancellationToken)
        {
            await _taskManagerBusinessRules.UserShouldExistWhenSelected(request.UserGid);
            await _taskManagerBusinessRules.IsTaskManagerExist(request.UserGid);

            TaskManager taskManager = new TaskManager
            {
                GidUserFK = request.UserGid,
            };

            await _taskManagerWriteRepository.AddAsync(taskManager);
            await _taskManagerWriteRepository.SaveAsync();
            GetByGidTaskManagerResponse obj = _mapper.Map<GetByGidTaskManagerResponse>(taskManager);
            return new()
            {
                Title = TaskManagersBusinessMessages.ProcessCompleted,
                Message = TaskManagersBusinessMessages.SuccessCreatedTaskManagerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}