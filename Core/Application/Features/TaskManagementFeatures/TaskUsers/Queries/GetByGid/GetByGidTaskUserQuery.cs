using AutoMapper;
using MediatR;
using X = Domain.Entities.TaskManagements;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using Application.Features.TaskManagementFeatures.TaskUsers.Rules;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid
{
    public class GetByGidTaskUserQuery : IRequest<GetByGidTaskUserResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTaskUserQueryHandler : IRequestHandler<GetByGidTaskUserQuery, GetByGidTaskUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITaskUserReadRepository _taskUserReadRepository;
            private readonly TaskUserBusinessRules _taskUserBusinessRules;

            public GetByGidTaskUserQueryHandler(IMapper mapper, ITaskUserReadRepository taskUserReadRepository, TaskUserBusinessRules taskUserBusinessRules)
            {
                _mapper = mapper;
                _taskUserReadRepository = taskUserReadRepository;
                _taskUserBusinessRules = taskUserBusinessRules;
            }

            public async Task<GetByGidTaskUserResponse> Handle(GetByGidTaskUserQuery request, CancellationToken cancellationToken)
            {
                await _taskUserBusinessRules.TaskUserShouldExistWhenSelected(request.Gid);

                X.TaskUser? taskUser = await _taskUserReadRepository.GetAsync(
                    predicate: uc => uc.Gid == request.Gid,
                    cancellationToken: cancellationToken//,
                                                        //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                                                        //TODO
                    );

                GetByGidTaskUserResponse response = _mapper.Map<GetByGidTaskUserResponse>(taskUser);
                return response;
            }
        }
    }
}