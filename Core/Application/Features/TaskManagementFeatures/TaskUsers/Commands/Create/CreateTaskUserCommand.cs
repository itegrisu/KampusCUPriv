using Application.Features.TaskManagementFeatures.TaskUsers.Constants;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Rules;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;
using Domain.Enums;
using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Create;

public class CreateTaskUserCommand : IRequest<CreatedTaskUserResponse>
{
    public Guid UserGid { get; set; }
    public Guid TaskGid { get; set; }


    public class CreateTaskUserCommandHandler : IRequestHandler<CreateTaskUserCommand, CreatedTaskUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskUserWriteRepository _taskUserWriteRepository;
        private readonly ITaskUserReadRepository _taskUserReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly TaskUserBusinessRules _taskUserBusinessRules;

        public CreateTaskUserCommandHandler(IMapper mapper, ITaskUserWriteRepository taskUserWriteRepository,
                                         TaskUserBusinessRules taskUserBusinessRules, ITaskUserReadRepository taskUserReadRepository, IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository)
        {
            _mapper = mapper;
            _taskUserWriteRepository = taskUserWriteRepository;
            _taskUserBusinessRules = taskUserBusinessRules;
            _taskUserReadRepository = taskUserReadRepository;
            _userReadRepository = userReadRepository;
            _taskReadRepository = taskReadRepository;
        }

        public async Task<CreatedTaskUserResponse> Handle(CreateTaskUserCommand request, CancellationToken cancellationToken)
        {
            await _taskUserBusinessRules.UserShouldExistWhenSelected(request.UserGid);
            await _taskUserBusinessRules.TaskShouldExistWhenSelected(request.TaskGid);
            await _taskUserBusinessRules.TaskUserShouldNotExistWhenSelected(request.UserGid, request.TaskGid);

            X.TaskUser taskUser = new X.TaskUser
            {
                GidUserFK = request.UserGid,
                GidTaskFK = request.TaskGid,
                TaskState = EnumTaskState.Continues
            };

            await _taskUserWriteRepository.AddAsync(taskUser);
            await _taskUserWriteRepository.SaveAsync();
            GetByGidTaskUserResponse obj = _mapper.Map<GetByGidTaskUserResponse>(taskUser);
            return new()
            {
                Title = TaskUsersBusinessMessages.ProcessCompleted,
                Message = TaskUsersBusinessMessages.SuccessCreatedTaskUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}