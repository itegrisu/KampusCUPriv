using Application.Features.LogManagementFeatures.LogFailedLogins.Constants;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogFailedLogins.Rules;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Create;

public class CreateLogFailedLoginCommand : IRequest<CreatedLogFailedLoginResponse>
{

    public string Email { get; set; }
    public string Password { get; set; }
    public string? IpAddress { get; set; }
    public string? Description { get; set; }



    public class CreateLogFailedLoginCommandHandler : IRequestHandler<CreateLogFailedLoginCommand, CreatedLogFailedLoginResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogFailedLoginWriteRepository _logFailedLoginWriteRepository;
        private readonly ILogFailedLoginReadRepository _logFailedLoginReadRepository;
        private readonly LogFailedLoginBusinessRules _logFailedLoginBusinessRules;

        public CreateLogFailedLoginCommandHandler(IMapper mapper, ILogFailedLoginWriteRepository logFailedLoginWriteRepository,
                                         LogFailedLoginBusinessRules logFailedLoginBusinessRules, ILogFailedLoginReadRepository logFailedLoginReadRepository)
        {
            _mapper = mapper;
            _logFailedLoginWriteRepository = logFailedLoginWriteRepository;
            _logFailedLoginBusinessRules = logFailedLoginBusinessRules;
            _logFailedLoginReadRepository = logFailedLoginReadRepository;
        }

        public async Task<CreatedLogFailedLoginResponse> Handle(CreateLogFailedLoginCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _logFailedLoginReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.LogFailedLogin logFailedLogin = _mapper.Map<X.LogFailedLogin>(request);
            //logFailedLogin.RowNo = maxRowNo + 1;

            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
            await _logFailedLoginWriteRepository.SaveAsync();

            X.LogFailedLogin savedLogFailedLogin = await _logFailedLoginReadRepository.GetAsync(predicate: x => x.Gid == logFailedLogin.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidLogFailedLoginResponse obj = _mapper.Map<GetByGidLogFailedLoginResponse>(savedLogFailedLogin);
            return new()
            {
                Title = LogFailedLoginsBusinessMessages.ProcessCompleted,
                Message = LogFailedLoginsBusinessMessages.SuccessCreatedLogFailedLoginMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}