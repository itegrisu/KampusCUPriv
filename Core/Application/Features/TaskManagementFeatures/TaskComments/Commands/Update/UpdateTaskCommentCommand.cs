using Application.Features.TaskManagementFeatures.TaskComments.Constants;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskComments.Rules;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using AutoMapper;
using Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Update;

public class UpdateTaskCommentCommand : IRequest<UpdatedTaskCommentResponse>
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public Guid GidTaskFK { get; set; }
    public string Comment { get; set; }

    public class UpdateTaskCommentCommandHandler : IRequestHandler<UpdateTaskCommentCommand, UpdatedTaskCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskCommentReadRepository _taskCommentReadRepository;
        private readonly ITaskCommentWriteRepository _taskCommentWriteRepository;
        private readonly TaskCommentBusinessRules _taskCommentBusinessRules;

        public UpdateTaskCommentCommandHandler(IMapper mapper, ITaskCommentReadRepository taskCommentReadRepository,
                                         TaskCommentBusinessRules taskCommentBusinessRules, ITaskCommentWriteRepository taskCommentWriteRepository)
        {
            _mapper = mapper;
            _taskCommentReadRepository = taskCommentReadRepository;
            _taskCommentBusinessRules = taskCommentBusinessRules;
            _taskCommentWriteRepository = taskCommentWriteRepository;
        }

        public async Task<UpdatedTaskCommentResponse> Handle(UpdateTaskCommentCommand request, CancellationToken cancellationToken)
        {
            await _taskCommentBusinessRules.TaskCommentShouldExistWhenSelected(request.Gid);
            await _taskCommentBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            await _taskCommentBusinessRules.TaskShouldExistWhenSelected(request.GidTaskFK);

            TaskComment? taskComment = await _taskCommentReadRepository.GetAsync(predicate: tc => tc.Gid == request.Gid, cancellationToken: cancellationToken);
            taskComment = _mapper.Map(request, taskComment);

            _taskCommentWriteRepository.Update(taskComment!);
            await _taskCommentWriteRepository.SaveAsync();
            GetByGidTaskCommentResponse obj = _mapper.Map<GetByGidTaskCommentResponse>(taskComment);

            return new()
            {
                Title = TaskCommentsBusinessMessages.ProcessCompleted,
                Message = TaskCommentsBusinessMessages.SuccessUpdatedTaskCommentMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}