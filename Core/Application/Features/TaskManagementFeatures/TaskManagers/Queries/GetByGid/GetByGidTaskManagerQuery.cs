using AutoMapper;
using MediatR;
using X = Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using Application.Features.TaskManagementFeatures.TaskManagers.Rules;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetByGid
{
    public class GetByGidTaskManagerQuery : IRequest<GetByGidTaskManagerResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTaskManagerQueryHandler : IRequestHandler<GetByGidTaskManagerQuery, GetByGidTaskManagerResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITaskManagerReadRepository _taskManagerReadRepository;
            private readonly TaskManagerBusinessRules _taskManagerBusinessRules;

            public GetByGidTaskManagerQueryHandler(IMapper mapper, ITaskManagerReadRepository taskManagerReadRepository, TaskManagerBusinessRules taskManagerBusinessRules)
            {
                _mapper = mapper;
                _taskManagerReadRepository = taskManagerReadRepository;
                _taskManagerBusinessRules = taskManagerBusinessRules;
            }

            public async Task<GetByGidTaskManagerResponse> Handle(GetByGidTaskManagerQuery request, CancellationToken cancellationToken)
            {
                await _taskManagerBusinessRules.TaskManagerShouldExistWhenSelected(request.Gid);

                X.TaskManager? taskManager = await _taskManagerReadRepository.GetAsync(
                    predicate: uc => uc.Gid == request.Gid,
                    cancellationToken: cancellationToken,
                    include: i => i.Include(i => i.UserFK)
                    );

                GetByGidTaskManagerResponse response = _mapper.Map<GetByGidTaskManagerResponse>(taskManager);
                return response;
            }
        }
    }
}