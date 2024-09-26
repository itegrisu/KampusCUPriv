using Application.Features.TaskManagementFeatures.TaskUsers.Constants;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Rules;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;
using Domain.Enums;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Update;

public class UpdateTaskUserCommand : IRequest<UpdatedTaskUserResponse>
{
    public Guid Gid { get; set; }
    public Guid UserGid { get; set; }
    public Guid TaskGid { get; set; }
    public EnumTaskState TaskState { get; set; }
    public string? StatusNote { get; set; }

    public class UpdateTaskUserCommandHandler : IRequestHandler<UpdateTaskUserCommand, UpdatedTaskUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskUserWriteRepository _taskUserWriteRepository;
        private readonly ITaskUserReadRepository _taskUserReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly TaskUserBusinessRules _taskUserBusinessRules;

        public UpdateTaskUserCommandHandler(IMapper mapper, ITaskUserWriteRepository taskUserWriteRepository,
                                         TaskUserBusinessRules taskUserBusinessRules, ITaskUserReadRepository taskUserReadRepository, IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository)
        {
            _mapper = mapper;
            _taskUserWriteRepository = taskUserWriteRepository;
            _taskUserBusinessRules = taskUserBusinessRules;
            _taskUserReadRepository = taskUserReadRepository;
            _userReadRepository = userReadRepository;
            _taskReadRepository = taskReadRepository;
        }

        public async Task<UpdatedTaskUserResponse> Handle(UpdateTaskUserCommand request, CancellationToken cancellationToken)
        {
            await _taskUserBusinessRules.TaskUserShouldExistWhenSelected(request.Gid);

            await _taskUserBusinessRules.UserShouldExistWhenSelected(request.UserGid);
            await _taskUserBusinessRules.TaskShouldExistWhenSelected(request.TaskGid);

            X.TaskUser? taskUser = await _taskUserReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            taskUser.TaskState = request.TaskState;
            taskUser.GidTaskFK = request.TaskGid;
            taskUser.GidUserFK = request.UserGid;
            taskUser.StatusNote = request.StatusNote;
            _taskUserWriteRepository.Update(taskUser!);

            await _taskUserWriteRepository.SaveAsync();
            GetByGidTaskUserResponse obj = _mapper.Map<GetByGidTaskUserResponse>(taskUser);

            return new()
            {
                Title = TaskUsersBusinessMessages.ProcessCompleted,
                Message = TaskUsersBusinessMessages.SuccessUpdatedTaskUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}