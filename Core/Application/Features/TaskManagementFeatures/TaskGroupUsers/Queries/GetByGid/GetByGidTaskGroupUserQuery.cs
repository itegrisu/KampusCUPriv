using AutoMapper;
using MediatR;
using X = Domain.Entities.TaskManagements;
using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Rules;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid
{
    public class GetByGidTaskGroupUserQuery : IRequest<GetByGidTaskGroupUserResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTaskGroupUserQueryHandler : IRequestHandler<GetByGidTaskGroupUserQuery, GetByGidTaskGroupUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITaskGroupUserReadRepository _taskGroupUserReadRepository;
            private readonly TaskGroupUserBusinessRules _taskGroupUserBusinessRules;

            public GetByGidTaskGroupUserQueryHandler(IMapper mapper, ITaskGroupUserReadRepository taskGroupUserReadRepository, TaskGroupUserBusinessRules taskGroupUserBusinessRules)
            {
                _mapper = mapper;
                _taskGroupUserReadRepository = taskGroupUserReadRepository;
                _taskGroupUserBusinessRules = taskGroupUserBusinessRules;
            }

            public async Task<GetByGidTaskGroupUserResponse> Handle(GetByGidTaskGroupUserQuery request, CancellationToken cancellationToken)
            {
                await _taskGroupUserBusinessRules.TaskGroupUserShouldExistWhenSelected(request.Gid);

                X.TaskGroupUser? taskGroupUser = await _taskGroupUserReadRepository.GetAsync(
                    predicate: uc => uc.Gid == request.Gid,
                    cancellationToken: cancellationToken//,
                                                        //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                                                        //TODO
                    );

                GetByGidTaskGroupUserResponse response = _mapper.Map<GetByGidTaskGroupUserResponse>(taskGroupUser);
                return response;
            }
        }
    }
}