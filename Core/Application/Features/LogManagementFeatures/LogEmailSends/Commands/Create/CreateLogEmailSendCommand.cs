using Application.Features.LogManagementFeatures.LogEmailSends.Constants;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogEmailSends.Rules;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Create;

public class CreateLogEmailSendCommand : IRequest<CreatedLogEmailSendResponse>
{
    public Guid GidUserFK { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }



    public class CreateLogEmailSendCommandHandler : IRequestHandler<CreateLogEmailSendCommand, CreatedLogEmailSendResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogEmailSendWriteRepository _logEmailSendWriteRepository;
        private readonly ILogEmailSendReadRepository _logEmailSendReadRepository;
        private readonly LogEmailSendBusinessRules _logEmailSendBusinessRules;

        public CreateLogEmailSendCommandHandler(IMapper mapper, ILogEmailSendWriteRepository logEmailSendWriteRepository,
                                         LogEmailSendBusinessRules logEmailSendBusinessRules, ILogEmailSendReadRepository logEmailSendReadRepository)
        {
            _mapper = mapper;
            _logEmailSendWriteRepository = logEmailSendWriteRepository;
            _logEmailSendBusinessRules = logEmailSendBusinessRules;
            _logEmailSendReadRepository = logEmailSendReadRepository;
        }

        public async Task<CreatedLogEmailSendResponse> Handle(CreateLogEmailSendCommand request, CancellationToken cancellationToken)
        {
            await _logEmailSendBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            X.LogEmailSend logEmailSend = _mapper.Map<X.LogEmailSend>(request);

            await _logEmailSendWriteRepository.AddAsync(logEmailSend);
            await _logEmailSendWriteRepository.SaveAsync();

            X.LogEmailSend? savedLogEmailSend = await _logEmailSendReadRepository.GetAsync(predicate: x => x.Gid == logEmailSend.Gid, include: x=>x.Include(x=>x.UserFK));

            GetByGidLogEmailSendResponse obj = _mapper.Map<GetByGidLogEmailSendResponse>(savedLogEmailSend);
            return new()
            {
                Title = LogEmailSendsBusinessMessages.ProcessCompleted,
                Message = LogEmailSendsBusinessMessages.SuccessCreatedLogEmailSendMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}