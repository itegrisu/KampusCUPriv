using Application.Features.TaskManagementFeatures.TaskComments.Constants;
using Application.Features.TaskManagementFeatures.TaskComments.Rules;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using AutoMapper;
using Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Delete;

public class DeleteTaskCommentCommand : IRequest<DeletedTaskCommentResponse>
{
    public Guid Gid { get; set; }

    public class DeleteTaskCommentCommandHandler : IRequestHandler<DeleteTaskCommentCommand, DeletedTaskCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskCommentReadRepository _taskCommentReadRepository;
        private readonly ITaskCommentWriteRepository _taskCommentWriteRepository;
        private readonly TaskCommentBusinessRules _taskCommentBusinessRules;

        public DeleteTaskCommentCommandHandler(IMapper mapper, ITaskCommentReadRepository taskCommentReadRepository,
                                         TaskCommentBusinessRules taskCommentBusinessRules, ITaskCommentWriteRepository taskCommentWriteRepository)
        {
            _mapper = mapper;
            _taskCommentReadRepository = taskCommentReadRepository;
            _taskCommentBusinessRules = taskCommentBusinessRules;
            _taskCommentWriteRepository = taskCommentWriteRepository;
        }

        public async Task<DeletedTaskCommentResponse> Handle(DeleteTaskCommentCommand request, CancellationToken cancellationToken)
        {
            await _taskCommentBusinessRules.TaskCommentShouldExistWhenSelected(request.Gid);

            TaskComment? taskComment = await _taskCommentReadRepository.GetAsync(predicate: tc => tc.Gid == request.Gid, cancellationToken: cancellationToken);

            taskComment.DataState = Core.Enum.DataState.Deleted;
            _taskCommentWriteRepository.Update(taskComment);
            await _taskCommentWriteRepository.SaveAsync();

            return new()
            {
                Title = TaskCommentsBusinessMessages.ProcessCompleted,
                Message = TaskCommentsBusinessMessages.SuccessDeletedTaskCommentMessage,
                IsValid = true
            };
        }
    }
}