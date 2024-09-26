using Application.Repositories.TaskManagementRepos.TaskRepo;
using AutoMapper;
using T = Domain.Entities.TaskManagements;
using Domain.Enums;
using MediatR;
using Application.Features.TaskManagementFeatures.Tasks.Rules;
using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Create;

public class CreateTaskCommand : IRequest<CreatedTaskResponse>
{
    public Guid TaskAssignerUserGid { get; set; }
    public string Title { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public EnumPriorityType PriorityType { get; set; }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreatedTaskResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskWriteRepository _taskWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly TaskBusinessRules _taskBusinessRules;

        public CreateTaskCommandHandler(IMapper mapper, ITaskWriteRepository taskWriteRepository,
                                         TaskBusinessRules taskBusinessRules, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _taskWriteRepository = taskWriteRepository;
            _taskBusinessRules = taskBusinessRules;
            _userReadRepository = userReadRepository;
        }

        public async Task<CreatedTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskBusinessRules.UserShouldExistWhenSelected(request.TaskAssignerUserGid);
            await _taskBusinessRules.isTaskManager(request.TaskAssignerUserGid);

            T.Task task = _mapper.Map<T.Task>(request);
            task.GidTaskAssignerUserFK = request.TaskAssignerUserGid;

            await _taskWriteRepository.AddAsync(task);
            await _taskWriteRepository.SaveAsync();

            GetByGidTaskResponse obj = _mapper.Map<GetByGidTaskResponse>(task);
            obj.EndDate = request.EndDate;
            return new()
            {
                Title = TasksBusinessMessages.ProcessCompleted,
                Message = TasksBusinessMessages.SuccessCreatedTaskMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}