using Application.Features.TaskManagementFeatures.TaskComments.Rules;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using AutoMapper;
using Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;

public class GetByGidTaskCommentQuery : IRequest<GetByGidTaskCommentResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdTaskCommentQueryHandler : IRequestHandler<GetByGidTaskCommentQuery, GetByGidTaskCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskCommentReadRepository _taskCommentReadRepository;
        private readonly TaskCommentBusinessRules _taskCommentBusinessRules;

        public GetByIdTaskCommentQueryHandler(IMapper mapper, ITaskCommentReadRepository taskCommentReadRepository, TaskCommentBusinessRules taskCommentBusinessRules)
        {
            _mapper = mapper;
            _taskCommentReadRepository = taskCommentReadRepository;
            _taskCommentBusinessRules = taskCommentBusinessRules;
        }

        public async Task<GetByGidTaskCommentResponse> Handle(GetByGidTaskCommentQuery request, CancellationToken cancellationToken)
        {
            await _taskCommentBusinessRules.TaskCommentShouldExistWhenSelected(request.Gid);
            TaskComment? taskComment = await _taskCommentReadRepository.GetAsync(predicate: tc => tc.Gid == request.Gid, cancellationToken: cancellationToken);

            GetByGidTaskCommentResponse response = _mapper.Map<GetByGidTaskCommentResponse>(taskComment);
            return response;
        }
    }
}