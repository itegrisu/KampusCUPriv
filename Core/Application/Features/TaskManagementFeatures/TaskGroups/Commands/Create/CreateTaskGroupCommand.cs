using Application.Features.TaskManagementFeatures.TaskGroups.Constants;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroups.Rules;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Create;

public class CreateTaskGroupCommand : IRequest<CreatedTaskGroupResponse>
{
    public string GroupName { get; set; }

    public class CreateTaskGroupCommandHandler : IRequestHandler<CreateTaskGroupCommand, CreatedTaskGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskGroupWriteRepository _taskGroupWriteRepository;
        private readonly ITaskGroupReadRepository _taskGroupReadRepository;
        private readonly TaskGroupBusinessRules _taskGroupBusinessRules;

        public CreateTaskGroupCommandHandler(IMapper mapper, ITaskGroupWriteRepository taskGroupWriteRepository,
                                         TaskGroupBusinessRules taskGroupBusinessRules, ITaskGroupReadRepository taskGroupReadRepository)
        {
            _mapper = mapper;
            _taskGroupWriteRepository = taskGroupWriteRepository;
            _taskGroupBusinessRules = taskGroupBusinessRules;
            _taskGroupReadRepository = taskGroupReadRepository;
        }

        public async Task<CreatedTaskGroupResponse> Handle(CreateTaskGroupCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _taskGroupReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.TaskGroup taskGroup = _mapper.Map<X.TaskGroup>(request);
            //taskGroup.RowNo = maxRowNo + 1;

            await _taskGroupWriteRepository.AddAsync(taskGroup);
            await _taskGroupWriteRepository.SaveAsync();
            GetByGidTaskGroupResponse obj = _mapper.Map<GetByGidTaskGroupResponse>(taskGroup);
            return new()
            {
                Title = TaskGroupsBusinessMessages.ProcessCompleted,
                Message = TaskGroupsBusinessMessages.SuccessCreatedTaskGroupMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}