using Application.Features.DefinitionManagementFeatures.JobTypes.Constants;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.JobTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Update;

public class UpdateJobTypeCommand : IRequest<UpdatedJobTypeResponse>
{
    public Guid Gid { get; set; }
    public string GorevAdi { get; set; }

    public class UpdateJobTypeCommandHandler : IRequestHandler<UpdateJobTypeCommand, UpdatedJobTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJobTypeWriteRepository _jobTypeWriteRepository;
        private readonly IJobTypeReadRepository _jobTypeReadRepository;
        private readonly JobTypeBusinessRules _jobTypeBusinessRules;

        public UpdateJobTypeCommandHandler(IMapper mapper, IJobTypeWriteRepository jobTypeWriteRepository,
                                         JobTypeBusinessRules jobTypeBusinessRules, IJobTypeReadRepository jobTypeReadRepository)
        {
            _mapper = mapper;
            _jobTypeWriteRepository = jobTypeWriteRepository;
            _jobTypeBusinessRules = jobTypeBusinessRules;
            _jobTypeReadRepository = jobTypeReadRepository;
        }

        public async Task<UpdatedJobTypeResponse> Handle(UpdateJobTypeCommand request, CancellationToken cancellationToken)
        {
            X.JobType? jobType = await _jobTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _jobTypeBusinessRules.JobTypeShouldExistWhenSelected(jobType);
            await _jobTypeBusinessRules.CheckJobTypeNameIsUnique(request.GorevAdi, request.Gid);

            jobType = _mapper.Map(request, jobType);

            _jobTypeWriteRepository.Update(jobType!);
            await _jobTypeWriteRepository.SaveAsync();
            GetByGidJobTypeResponse obj = _mapper.Map<GetByGidJobTypeResponse>(jobType);

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