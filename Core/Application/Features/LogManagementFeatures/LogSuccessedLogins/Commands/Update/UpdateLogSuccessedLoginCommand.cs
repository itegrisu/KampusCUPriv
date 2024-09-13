using Application.Features.LogManagementFeatures.LogSuccessedLogins.Constants;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Rules;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Update;

public class UpdateLogSuccessedLoginCommand : IRequest<UpdatedLogSuccessedLoginResponse>
{
    public Guid Gid { get; set; }

	public Guid GidUserFK { get; set; }

public string? IpAddress { get; set; }
public string SessionId { get; set; }
public DateTime? LogOutDate { get; set; }



    public class UpdateLogSuccessedLoginCommandHandler : IRequestHandler<UpdateLogSuccessedLoginCommand, UpdatedLogSuccessedLoginResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogSuccessedLoginWriteRepository _logSuccessedLoginWriteRepository;
        private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
        private readonly LogSuccessedLoginBusinessRules _logSuccessedLoginBusinessRules;

        public UpdateLogSuccessedLoginCommandHandler(IMapper mapper, ILogSuccessedLoginWriteRepository logSuccessedLoginWriteRepository,
                                         LogSuccessedLoginBusinessRules logSuccessedLoginBusinessRules, ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository)
        {
            _mapper = mapper;
            _logSuccessedLoginWriteRepository = logSuccessedLoginWriteRepository;
            _logSuccessedLoginBusinessRules = logSuccessedLoginBusinessRules;
            _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
        }

        public async Task<UpdatedLogSuccessedLoginResponse> Handle(UpdateLogSuccessedLoginCommand request, CancellationToken cancellationToken)
        {
            X.LogSuccessedLogin? logSuccessedLogin = await _logSuccessedLoginReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _logSuccessedLoginBusinessRules.LogSuccessedLoginShouldExistWhenSelected(logSuccessedLogin);
            logSuccessedLogin = _mapper.Map(request, logSuccessedLogin);

            _logSuccessedLoginWriteRepository.Update(logSuccessedLogin!);
            await _logSuccessedLoginWriteRepository.SaveAsync();
            GetByGidLogSuccessedLoginResponse obj = _mapper.Map<GetByGidLogSuccessedLoginResponse>(logSuccessedLogin);

            return new()
            {
                Title = LogSuccessedLoginsBusinessMessages.ProcessCompleted,
                Message = LogSuccessedLoginsBusinessMessages.SuccessUpdatedLogSuccessedLoginMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}