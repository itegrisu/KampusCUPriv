using Application.Repositories.TaskManagementRepos.TaskRepo;
using AutoMapper;
using T = Domain.Entities.TaskManagements;
using MediatR;
using Application.Features.TaskManagementFeatures.Tasks.Rules;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;

public class GetByGidTaskQuery : IRequest<GetByGidTaskResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdTaskQueryHandler : IRequestHandler<GetByGidTaskQuery, GetByGidTaskResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly TaskBusinessRules _taskBusinessRules;

        public GetByIdTaskQueryHandler(IMapper mapper, ITaskReadRepository taskReadRepository, TaskBusinessRules taskBusinessRules)
        {
            _mapper = mapper;
            _taskReadRepository = taskReadRepository;
            _taskBusinessRules = taskBusinessRules;
        }

        public async Task<GetByGidTaskResponse> Handle(GetByGidTaskQuery request, CancellationToken cancellationToken)
        {
            await _taskBusinessRules.TaskShouldExistWhenSelected(request.Gid);

            T.Task? task = await _taskReadRepository.GetAsync(predicate: t => t.Gid == request.Gid, cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK));

            GetByGidTaskResponse response = _mapper.Map<GetByGidTaskResponse>(task);
            return response;
        }
    }
}