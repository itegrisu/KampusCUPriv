using Application.Features.DefinitionManagementFeatures.JobTypes.Constants;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.JobTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Create;

public class CreateJobTypeCommand : IRequest<CreatedJobTypeResponse>
{

    public string Name { get; set; }

    public class CreateJobTypeCommandHandler : IRequestHandler<CreateJobTypeCommand, CreatedJobTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJobTypeWriteRepository _jobTypeWriteRepository;
        private readonly IJobTypeReadRepository _jobTypeReadRepository;
        private readonly JobTypeBusinessRules _jobTypeBusinessRules;

        public CreateJobTypeCommandHandler(IMapper mapper, IJobTypeWriteRepository jobTypeWriteRepository,
                                         JobTypeBusinessRules jobTypeBusinessRules, IJobTypeReadRepository jobTypeReadRepository)
        {
            _mapper = mapper;
            _jobTypeWriteRepository = jobTypeWriteRepository;
            _jobTypeBusinessRules = jobTypeBusinessRules;
            _jobTypeReadRepository = jobTypeReadRepository;
        }

        public async Task<CreatedJobTypeResponse> Handle(CreateJobTypeCommand request, CancellationToken cancellationToken)
        {
            await _jobTypeBusinessRules.CheckJobTypeNameIsUnique(request.Name);
            X.JobType jobType = _mapper.Map<X.JobType>(request);

            await _jobTypeWriteRepository.AddAsync(jobType);
            await _jobTypeWriteRepository.SaveAsync();

            X.JobType savedJobType = await _jobTypeReadRepository.GetAsync(predicate: x => x.Gid == jobType.Gid);

            GetByGidJobTypeResponse obj = _mapper.Map<GetByGidJobTypeResponse>(savedJobType);
            return new()
            {
                Title = JobTypesBusinessMessages.ProcessCompleted,
                Message = JobTypesBusinessMessages.SuccessCreatedJobTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}