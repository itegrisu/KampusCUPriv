using Application.Features.TaskManagementFeatures.TaskComments.Constants;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskComments.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using AutoMapper;
using Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Create;

public class CreateTaskCommentCommand : IRequest<CreatedTaskCommentResponse>
{
    public Guid UserGid { get; set; }
    public Guid TaskGid { get; set; }
    public string Comment { get; set; }

    public class CreateTaskCommentCommandHandler : IRequestHandler<CreateTaskCommentCommand, CreatedTaskCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskCommentWriteRepository _taskCommentWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly ITaskCommentReadRepository _taskCommentReadRepository;
        private readonly TaskCommentBusinessRules _taskCommentBusinessRules;

        public CreateTaskCommentCommandHandler(IMapper mapper, ITaskCommentWriteRepository taskCommentRepository,
                                         TaskCommentBusinessRules taskCommentBusinessRules, IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository, ITaskCommentReadRepository taskCommentReadRepository)
        {
            _mapper = mapper;
            _taskCommentWriteRepository = taskCommentRepository;
            _taskCommentBusinessRules = taskCommentBusinessRules;
            _userReadRepository = userReadRepository;
            _taskReadRepository = taskReadRepository;
            _taskCommentReadRepository = taskCommentReadRepository;
        }

        public async Task<CreatedTaskCommentResponse> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
        {
            await _taskCommentBusinessRules.UserShouldExistWhenSelected(request.UserGid);
            await _taskCommentBusinessRules.TaskShouldExistWhenSelected(request.TaskGid);

            TaskComment taskComment = new TaskComment
            {
                GidTaskFK = request.TaskGid,
                GidUserFK = request.UserGid,
                Comment = request.Comment
            };

            await _taskCommentWriteRepository.AddAsync(taskComment);
            await _taskCommentWriteRepository.SaveAsync();

            TaskComment SavedtaskComment2 = await _taskCommentReadRepository.GetAsync(predicate: x => x.Gid == taskComment.Gid);

            GetByGidTaskCommentResponse obj = _mapper.Map<GetByGidTaskCommentResponse>(SavedtaskComment2);
            return new()
            {
                Title = TaskCommentsBusinessMessages.ProcessCompleted,
                Message = TaskCommentsBusinessMessages.SuccessCreatedTaskCommentMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}