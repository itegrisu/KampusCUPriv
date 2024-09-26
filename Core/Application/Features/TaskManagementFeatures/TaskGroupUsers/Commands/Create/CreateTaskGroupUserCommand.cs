using Application.Features.TaskManagementFeatures.TaskGroupUsers.Constants;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Rules;
using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using AutoMapper;
using MediatR;
using Domain.Entities.TaskManagements;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Create;

public class CreateTaskGroupUserCommand : IRequest<CreatedTaskGroupUserResponse>
{
    public Guid TaskGroupGid { get; set; }
    public Guid UserGid { get; set; }


    public class CreateTaskGroupUserCommandHandler : IRequestHandler<CreateTaskGroupUserCommand, CreatedTaskGroupUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskGroupUserWriteRepository _taskGroupUserWriteRepository;
        private readonly ITaskGroupUserReadRepository _taskGroupUserReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ITaskGroupReadRepository _taskGroupReadRepository;
        private readonly TaskGroupUserBusinessRules _taskGroupUserBusinessRules;

        public CreateTaskGroupUserCommandHandler(IMapper mapper, ITaskGroupUserWriteRepository taskGroupUserWriteRepository,
                                         TaskGroupUserBusinessRules taskGroupUserBusinessRules, ITaskGroupUserReadRepository taskGroupUserReadRepository, IUserReadRepository userReadRepository, ITaskGroupReadRepository taskGroupReadRepository)
        {
            _mapper = mapper;
            _taskGroupUserWriteRepository = taskGroupUserWriteRepository;
            _taskGroupUserBusinessRules = taskGroupUserBusinessRules;
            _taskGroupUserReadRepository = taskGroupUserReadRepository;
            _userReadRepository = userReadRepository;
            _taskGroupReadRepository = taskGroupReadRepository;
        }

        public async Task<CreatedTaskGroupUserResponse> Handle(CreateTaskGroupUserCommand request, CancellationToken cancellationToken)
        {
            await _taskGroupUserBusinessRules.TaskGroupShouldExistWhenSelected(request.TaskGroupGid);
            await _taskGroupUserBusinessRules.UserShouldExistWhenSelected(request.UserGid);
            await _taskGroupUserBusinessRules.GroupUserAlreadyExist(request.UserGid, request.TaskGroupGid);


            TaskGroupUser taskGroupUser = new TaskGroupUser
            {
                GidTaskGroupFK = request.TaskGroupGid,
                GidUserFK = request.UserGid,
            };


            await _taskGroupUserWriteRepository.AddAsync(taskGroupUser);
            await _taskGroupUserWriteRepository.SaveAsync();
            GetByGidTaskGroupUserResponse obj = _mapper.Map<GetByGidTaskGroupUserResponse>(taskGroupUser);
            return new()
            {
                Title = TaskGroupUsersBusinessMessages.ProcessCompleted,
                Message = TaskGroupUsersBusinessMessages.SuccessCreatedTaskGroupUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}