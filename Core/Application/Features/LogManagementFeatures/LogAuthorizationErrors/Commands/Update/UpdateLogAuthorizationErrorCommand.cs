using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Constants;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Rules;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Update;

public class UpdateLogAuthorizationErrorCommand : IRequest<UpdatedLogAuthorizationErrorResponse>
{
    public Guid Gid { get; set; }

	public Guid GidUserFK { get; set; }

public string? IpAddress { get; set; }
public string PageInfo { get; set; }
public string? Operation { get; set; }
public string? JSonData { get; set; }



    public class UpdateLogAuthorizationErrorCommandHandler : IRequestHandler<UpdateLogAuthorizationErrorCommand, UpdatedLogAuthorizationErrorResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogAuthorizationErrorWriteRepository _logAuthorizationErrorWriteRepository;
        private readonly ILogAuthorizationErrorReadRepository _logAuthorizationErrorReadRepository;
        private readonly LogAuthorizationErrorBusinessRules _logAuthorizationErrorBusinessRules;

        public UpdateLogAuthorizationErrorCommandHandler(IMapper mapper, ILogAuthorizationErrorWriteRepository logAuthorizationErrorWriteRepository,
                                         LogAuthorizationErrorBusinessRules logAuthorizationErrorBusinessRules, ILogAuthorizationErrorReadRepository logAuthorizationErrorReadRepository)
        {
            _mapper = mapper;
            _logAuthorizationErrorWriteRepository = logAuthorizationErrorWriteRepository;
            _logAuthorizationErrorBusinessRules = logAuthorizationErrorBusinessRules;
            _logAuthorizationErrorReadRepository = logAuthorizationErrorReadRepository;
        }

        public async Task<UpdatedLogAuthorizationErrorResponse> Handle(UpdateLogAuthorizationErrorCommand request, CancellationToken cancellationToken)
        {
            X.LogAuthorizationError? logAuthorizationError = await _logAuthorizationErrorReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _logAuthorizationErrorBusinessRules.LogAuthorizationErrorShouldExistWhenSelected(logAuthorizationError);
            logAuthorizationError = _mapper.Map(request, logAuthorizationError);

            _logAuthorizationErrorWriteRepository.Update(logAuthorizationError!);
            await _logAuthorizationErrorWriteRepository.SaveAsync();
            GetByGidLogAuthorizationErrorResponse obj = _mapper.Map<GetByGidLogAuthorizationErrorResponse>(logAuthorizationError);

            return new()
            {
                Title = LogAuthorizationErrorsBusinessMessages.ProcessCompleted,
                Message = LogAuthorizationErrorsBusinessMessages.SuccessUpdatedLogAuthorizationErrorMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}