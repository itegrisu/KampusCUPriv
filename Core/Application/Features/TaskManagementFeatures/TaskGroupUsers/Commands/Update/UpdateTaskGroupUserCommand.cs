using Application.Features.TaskManagementFeatures.TaskGroupUsers.Constants;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Rules;
using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Update;

public class UpdateTaskGroupUserCommand : IRequest<UpdatedTaskGroupUserResponse>
{
    public Guid Gid { get; set; }
    public Guid GidTaskGroupFK { get; set; }
    public Guid GidUserFK { get; set; }

    public class UpdateTaskGroupUserCommandHandler : IRequestHandler<UpdateTaskGroupUserCommand, UpdatedTaskGroupUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskGroupUserWriteRepository _taskGroupUserWriteRepository;
        private readonly ITaskGroupUserReadRepository _taskGroupUserReadRepository;
        private readonly TaskGroupUserBusinessRules _taskGroupUserBusinessRules;

        public UpdateTaskGroupUserCommandHandler(IMapper mapper, ITaskGroupUserWriteRepository taskGroupUserWriteRepository,
                                         TaskGroupUserBusinessRules taskGroupUserBusinessRules, ITaskGroupUserReadRepository taskGroupUserReadRepository)
        {
            _mapper = mapper;
            _taskGroupUserWriteRepository = taskGroupUserWriteRepository;
            _taskGroupUserBusinessRules = taskGroupUserBusinessRules;
            _taskGroupUserReadRepository = taskGroupUserReadRepository;
        }

        public async Task<UpdatedTaskGroupUserResponse> Handle(UpdateTaskGroupUserCommand request, CancellationToken cancellationToken)
        {
            await _taskGroupUserBusinessRules.TaskGroupUserShouldExistWhenSelected(request.Gid);
            await _taskGroupUserBusinessRules.TaskGroupShouldExistWhenSelected(request.GidTaskGroupFK);
            await _taskGroupUserBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            await _taskGroupUserBusinessRules.GroupUserAlreadyExist(request.GidUserFK, request.GidTaskGroupFK);

            X.TaskGroupUser? taskGroupUser = await _taskGroupUserReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken); //, //TODO);

            taskGroupUser = _mapper.Map(request, taskGroupUser);

            _taskGroupUserWriteRepository.Update(taskGroupUser!);
            await _taskGroupUserWriteRepository.SaveAsync();
            GetByGidTaskGroupUserResponse obj = _mapper.Map<GetByGidTaskGroupUserResponse>(taskGroupUser);

            return new()
            {
                Title = TaskGroupUsersBusinessMessages.ProcessCompleted,
                Message = TaskGroupUsersBusinessMessages.SuccessUpdatedTaskGroupUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}