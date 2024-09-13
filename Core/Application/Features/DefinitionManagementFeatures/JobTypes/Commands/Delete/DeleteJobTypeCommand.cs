using Application.Features.DefinitionManagementFeatures.JobTypes.Constants;
using Application.Features.DefinitionManagementFeatures.JobTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.JobTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Delete;

public class DeleteJobTypeCommand : IRequest<DeletedJobTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteJobTypeCommandHandler : IRequestHandler<DeleteJobTypeCommand, DeletedJobTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJobTypeReadRepository _jobTypeReadRepository;
        private readonly IJobTypeWriteRepository _jobTypeWriteRepository;
        private readonly JobTypeBusinessRules _jobTypeBusinessRules;

        public DeleteJobTypeCommandHandler(IMapper mapper, IJobTypeReadRepository jobTypeReadRepository,
                                         JobTypeBusinessRules jobTypeBusinessRules, IJobTypeWriteRepository jobTypeWriteRepository)
        {
            _mapper = mapper;
            _jobTypeReadRepository = jobTypeReadRepository;
            _jobTypeBusinessRules = jobTypeBusinessRules;
            _jobTypeWriteRepository = jobTypeWriteRepository;
        }

        public async Task<DeletedJobTypeResponse> Handle(DeleteJobTypeCommand request, CancellationToken cancellationToken)
        {
            X.JobType? jobType = await _jobTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _jobTypeBusinessRules.JobTypeShouldExistWhenSelected(jobType);
            jobType.DataState = Core.Enum.DataState.Deleted;

            _jobTypeWriteRepository.Update(jobType);
            await _jobTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = JobTypesBusinessMessages.ProcessCompleted,
                Message = JobTypesBusinessMessages.SuccessDeletedJobTypeMessage,
                IsValid = true
            };
        }
    }
}