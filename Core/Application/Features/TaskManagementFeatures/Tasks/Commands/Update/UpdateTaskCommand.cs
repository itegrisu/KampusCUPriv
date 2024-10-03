using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.Tasks.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using T = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Update;

public class UpdateTaskCommand : IRequest<UpdatedTaskResponse>
{
    public Guid Gid { get; set; }
    public Guid TaskAssignerUserGid { get; set; }
    public string Title { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public EnumPriorityType PriorityType { get; set; }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, UpdatedTaskResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly ITaskWriteRepository _taskWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly TaskBusinessRules _taskBusinessRules;

        public UpdateTaskCommandHandler(IMapper mapper, ITaskReadRepository taskReadRepository,
                                         TaskBusinessRules taskBusinessRules, ITaskWriteRepository taskWriteRepository, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _taskReadRepository = taskReadRepository;
            _taskBusinessRules = taskBusinessRules;
            _taskWriteRepository = taskWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<UpdatedTaskResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskBusinessRules.TaskShouldExistWhenSelected(request.Gid);
            await _taskBusinessRules.UserShouldExistWhenSelected(request.TaskAssignerUserGid);
            await _taskBusinessRules.IsTaskModuleAuth(request.TaskAssignerUserGid);


            T.Task? task = await _taskReadRepository.GetAsync(predicate: t => t.Gid == request.Gid, cancellationToken: cancellationToken);

            task = _mapper.Map(request, task);
            _taskWriteRepository.Update(task!);
            await _taskWriteRepository.SaveAsync();
            GetByGidTaskResponse obj = _mapper.Map<GetByGidTaskResponse>(task);

            return new()
            {
                Title = TasksBusinessMessages.ProcessCompleted,
                Message = TasksBusinessMessages.SuccessUpdatedTaskMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}