using Application.Features.DefinitionManagementFeatures.JobTypes.Constants;
using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Rules;

public class JobTypeBusinessRules : BaseBusinessRules
{

    private readonly IJobTypeReadRepository _jobTypeReadRepository;

    public JobTypeBusinessRules(IJobTypeReadRepository jobTypeReadRepository)
    {
        _jobTypeReadRepository = jobTypeReadRepository;
    }

    public async Task JobTypeShouldExistWhenSelected(X.JobType? item)
    {
        if (item == null)
            throw new BusinessException(JobTypesBusinessMessages.JobTypeNotExists);
    }

    public async Task CheckJobTypeNameIsUnique(string jobTypeName, Guid? jobTypeGuid = null)
    {
        var jobType = await _jobTypeReadRepository.GetAsync(predicate: x => x.Name.ToLower() == jobTypeName.ToLower() && (jobTypeGuid == null || x.Gid != jobTypeGuid));
        if (jobType != null)
            throw new BusinessException(JobTypesBusinessMessages.JobTypeIsAlreadyExists);
    }
}