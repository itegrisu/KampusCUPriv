using Application.Features.DefinitionManagementFeatures.JobTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid
{
    public class GetByGidJobTypeQuery : IRequest<GetByGidJobTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidJobTypeQueryHandler : IRequestHandler<GetByGidJobTypeQuery, GetByGidJobTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IJobTypeReadRepository _jobTypeReadRepository;
            private readonly JobTypeBusinessRules _jobTypeBusinessRules;

            public GetByGidJobTypeQueryHandler(IMapper mapper, IJobTypeReadRepository jobTypeReadRepository, JobTypeBusinessRules jobTypeBusinessRules)
            {
                _mapper = mapper;
                _jobTypeReadRepository = jobTypeReadRepository;
                _jobTypeBusinessRules = jobTypeBusinessRules;
            }

            public async Task<GetByGidJobTypeResponse> Handle(GetByGidJobTypeQuery request, CancellationToken cancellationToken)
            {
                X.JobType? jobType = await _jobTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _jobTypeBusinessRules.JobTypeShouldExistWhenSelected(jobType);

                GetByGidJobTypeResponse response = _mapper.Map<GetByGidJobTypeResponse>(jobType);
                return response;
            }
        }
    }
}