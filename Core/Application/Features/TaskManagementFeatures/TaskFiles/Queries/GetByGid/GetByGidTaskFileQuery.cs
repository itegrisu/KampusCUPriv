using AutoMapper;
using MediatR;
using X = Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using Application.Features.TaskManagementFeatures.TaskFiles.Rules;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetByGid
{
    public class GetByGidTaskFileQuery : IRequest<GetByGidTaskFileResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTaskFileQueryHandler : IRequestHandler<GetByGidTaskFileQuery, GetByGidTaskFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITaskFileReadRepository _taskFileReadRepository;
            private readonly TaskFileBusinessRules _taskFileBusinessRules;

            public GetByGidTaskFileQueryHandler(IMapper mapper, ITaskFileReadRepository taskFileReadRepository, TaskFileBusinessRules taskFileBusinessRules)
            {
                _mapper = mapper;
                _taskFileReadRepository = taskFileReadRepository;
                _taskFileBusinessRules = taskFileBusinessRules;
            }

            public async Task<GetByGidTaskFileResponse> Handle(GetByGidTaskFileQuery request, CancellationToken cancellationToken)
            {
                await _taskFileBusinessRules.TaskFileShouldExistWhenSelected(request.Gid);

                X.TaskFile? taskFile = await _taskFileReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                    include: i => i.Include(i => i.TaskFK).Include(i => i.UserFK));

                GetByGidTaskFileResponse response = _mapper.Map<GetByGidTaskFileResponse>(taskFile);
                return response;
            }
        }
    }
}