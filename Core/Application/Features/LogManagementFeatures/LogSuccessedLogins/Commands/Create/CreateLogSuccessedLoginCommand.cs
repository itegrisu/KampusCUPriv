using Application.Features.LogManagementFeatures.LogSuccessedLogins.Constants;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Rules;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Create;

public class CreateLogSuccessedLoginCommand : IRequest<CreatedLogSuccessedLoginResponse>
{
    public Guid GidUserFK { get; set; }

public string? IpAddress { get; set; }
public string SessionId { get; set; }
public DateTime? LogOutDate { get; set; }



    public class CreateLogSuccessedLoginCommandHandler : IRequestHandler<CreateLogSuccessedLoginCommand, CreatedLogSuccessedLoginResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogSuccessedLoginWriteRepository _logSuccessedLoginWriteRepository;
        private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
        private readonly LogSuccessedLoginBusinessRules _logSuccessedLoginBusinessRules;

        public CreateLogSuccessedLoginCommandHandler(IMapper mapper, ILogSuccessedLoginWriteRepository logSuccessedLoginWriteRepository,
                                         LogSuccessedLoginBusinessRules logSuccessedLoginBusinessRules, ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository)
        {
            _mapper = mapper;
            _logSuccessedLoginWriteRepository = logSuccessedLoginWriteRepository;
            _logSuccessedLoginBusinessRules = logSuccessedLoginBusinessRules;
            _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
        }

        public async Task<CreatedLogSuccessedLoginResponse> Handle(CreateLogSuccessedLoginCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _logSuccessedLoginReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.LogSuccessedLogin logSuccessedLogin = _mapper.Map<X.LogSuccessedLogin>(request);
            //logSuccessedLogin.RowNo = maxRowNo + 1;

            await _logSuccessedLoginWriteRepository.AddAsync(logSuccessedLogin);
            await _logSuccessedLoginWriteRepository.SaveAsync();

			X.LogSuccessedLogin savedLogSuccessedLogin = await _logSuccessedLoginReadRepository.GetAsync(predicate: x => x.Gid == logSuccessedLogin.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidLogSuccessedLoginResponse obj = _mapper.Map<GetByGidLogSuccessedLoginResponse>(savedLogSuccessedLogin);
            return new()
            {           
                Title = LogSuccessedLoginsBusinessMessages.ProcessCompleted,
                Message = LogSuccessedLoginsBusinessMessages.SuccessCreatedLogSuccessedLoginMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}