using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Constants;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Rules;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Update;

public class UpdateLogUserPageVisitActionCommand : IRequest<UpdatedLogUserPageVisitActionResponse>
{
    public Guid Gid { get; set; }

	public Guid GidUserFK { get; set; }

public string? IpAddress { get; set; }
public string PageInfo { get; set; }
public string Operation { get; set; }
public string JSonData { get; set; }



    public class UpdateLogUserPageVisitActionCommandHandler : IRequestHandler<UpdateLogUserPageVisitActionCommand, UpdatedLogUserPageVisitActionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogUserPageVisitActionWriteRepository _logUserPageVisitActionWriteRepository;
        private readonly ILogUserPageVisitActionReadRepository _logUserPageVisitActionReadRepository;
        private readonly LogUserPageVisitActionBusinessRules _logUserPageVisitActionBusinessRules;

        public UpdateLogUserPageVisitActionCommandHandler(IMapper mapper, ILogUserPageVisitActionWriteRepository logUserPageVisitActionWriteRepository,
                                         LogUserPageVisitActionBusinessRules logUserPageVisitActionBusinessRules, ILogUserPageVisitActionReadRepository logUserPageVisitActionReadRepository)
        {
            _mapper = mapper;
            _logUserPageVisitActionWriteRepository = logUserPageVisitActionWriteRepository;
            _logUserPageVisitActionBusinessRules = logUserPageVisitActionBusinessRules;
            _logUserPageVisitActionReadRepository = logUserPageVisitActionReadRepository;
        }

        public async Task<UpdatedLogUserPageVisitActionResponse> Handle(UpdateLogUserPageVisitActionCommand request, CancellationToken cancellationToken)
        {
            X.LogUserPageVisitAction? logUserPageVisitAction = await _logUserPageVisitActionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _logUserPageVisitActionBusinessRules.LogUserPageVisitActionShouldExistWhenSelected(logUserPageVisitAction);
            logUserPageVisitAction = _mapper.Map(request, logUserPageVisitAction);

            _logUserPageVisitActionWriteRepository.Update(logUserPageVisitAction!);
            await _logUserPageVisitActionWriteRepository.SaveAsync();
            GetByGidLogUserPageVisitActionResponse obj = _mapper.Map<GetByGidLogUserPageVisitActionResponse>(logUserPageVisitAction);

            return new()
            {
                Title = LogUserPageVisitActionsBusinessMessages.ProcessCompleted,
                Message = LogUserPageVisitActionsBusinessMessages.SuccessUpdatedLogUserPageVisitActionMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}