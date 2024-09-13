using Application.Features.LogManagementFeatures.LogUserPageVisits.Constants;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Rules;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Update;

public class UpdateLogUserPageVisitCommand : IRequest<UpdatedLogUserPageVisitResponse>
{
    public Guid Gid { get; set; }

	public Guid GidUserFK { get; set; }

public string? IpAddress { get; set; }
public string PageInfo { get; set; }
public string SessionId { get; set; }



    public class UpdateLogUserPageVisitCommandHandler : IRequestHandler<UpdateLogUserPageVisitCommand, UpdatedLogUserPageVisitResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogUserPageVisitWriteRepository _logUserPageVisitWriteRepository;
        private readonly ILogUserPageVisitReadRepository _logUserPageVisitReadRepository;
        private readonly LogUserPageVisitBusinessRules _logUserPageVisitBusinessRules;

        public UpdateLogUserPageVisitCommandHandler(IMapper mapper, ILogUserPageVisitWriteRepository logUserPageVisitWriteRepository,
                                         LogUserPageVisitBusinessRules logUserPageVisitBusinessRules, ILogUserPageVisitReadRepository logUserPageVisitReadRepository)
        {
            _mapper = mapper;
            _logUserPageVisitWriteRepository = logUserPageVisitWriteRepository;
            _logUserPageVisitBusinessRules = logUserPageVisitBusinessRules;
            _logUserPageVisitReadRepository = logUserPageVisitReadRepository;
        }

        public async Task<UpdatedLogUserPageVisitResponse> Handle(UpdateLogUserPageVisitCommand request, CancellationToken cancellationToken)
        {
            X.LogUserPageVisit? logUserPageVisit = await _logUserPageVisitReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _logUserPageVisitBusinessRules.LogUserPageVisitShouldExistWhenSelected(logUserPageVisit);
            logUserPageVisit = _mapper.Map(request, logUserPageVisit);

            _logUserPageVisitWriteRepository.Update(logUserPageVisit!);
            await _logUserPageVisitWriteRepository.SaveAsync();
            GetByGidLogUserPageVisitResponse obj = _mapper.Map<GetByGidLogUserPageVisitResponse>(logUserPageVisit);

            return new()
            {
                Title = LogUserPageVisitsBusinessMessages.ProcessCompleted,
                Message = LogUserPageVisitsBusinessMessages.SuccessUpdatedLogUserPageVisitMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}