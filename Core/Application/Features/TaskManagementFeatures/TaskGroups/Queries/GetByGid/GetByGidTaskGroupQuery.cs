using AutoMapper;
using MediatR;
using X = Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using Application.Features.TaskManagementFeatures.TaskGroups.Rules;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid
{
    public class GetByGidTaskGroupQuery : IRequest<GetByGidTaskGroupResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTaskGroupQueryHandler : IRequestHandler<GetByGidTaskGroupQuery, GetByGidTaskGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITaskGroupReadRepository _taskGroupReadRepository;
            private readonly TaskGroupBusinessRules _taskGroupBusinessRules;

            public GetByGidTaskGroupQueryHandler(IMapper mapper, ITaskGroupReadRepository taskGroupReadRepository, TaskGroupBusinessRules taskGroupBusinessRules)
            {
                _mapper = mapper;
                _taskGroupReadRepository = taskGroupReadRepository;
                _taskGroupBusinessRules = taskGroupBusinessRules;
            }

            public async Task<GetByGidTaskGroupResponse> Handle(GetByGidTaskGroupQuery request, CancellationToken cancellationToken)
            {
                await _taskGroupBusinessRules.TaskGroupShouldExistWhenSelected(request.Gid);

                X.TaskGroup? taskGroup = await _taskGroupReadRepository.GetAsync(
                    predicate: uc => uc.Gid == request.Gid,
                    cancellationToken: cancellationToken,
                    include: i => i.Include(i => i.TaskGroupUsers)
                    );

                GetByGidTaskGroupResponse response = _mapper.Map<GetByGidTaskGroupResponse>(taskGroup);
                return response;
            }
        }
    }
}