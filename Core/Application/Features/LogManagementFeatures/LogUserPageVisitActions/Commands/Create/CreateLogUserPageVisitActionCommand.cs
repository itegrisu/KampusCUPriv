using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Constants;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Rules;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Create;

public class CreateLogUserPageVisitActionCommand : IRequest<CreatedLogUserPageVisitActionResponse>
{
    public Guid GidUserFK { get; set; }
    public string? IpAddress { get; set; }
    public string PageInfo { get; set; }
    public string Operation { get; set; }
    public string JSonData { get; set; }



    public class CreateLogUserPageVisitActionCommandHandler : IRequestHandler<CreateLogUserPageVisitActionCommand, CreatedLogUserPageVisitActionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogUserPageVisitActionWriteRepository _logUserPageVisitActionWriteRepository;
        private readonly ILogUserPageVisitActionReadRepository _logUserPageVisitActionReadRepository;
        private readonly LogUserPageVisitActionBusinessRules _logUserPageVisitActionBusinessRules;

        public CreateLogUserPageVisitActionCommandHandler(IMapper mapper, ILogUserPageVisitActionWriteRepository logUserPageVisitActionWriteRepository,
                                         LogUserPageVisitActionBusinessRules logUserPageVisitActionBusinessRules, ILogUserPageVisitActionReadRepository logUserPageVisitActionReadRepository)
        {
            _mapper = mapper;
            _logUserPageVisitActionWriteRepository = logUserPageVisitActionWriteRepository;
            _logUserPageVisitActionBusinessRules = logUserPageVisitActionBusinessRules;
            _logUserPageVisitActionReadRepository = logUserPageVisitActionReadRepository;
        }

        public async Task<CreatedLogUserPageVisitActionResponse> Handle(CreateLogUserPageVisitActionCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _logUserPageVisitActionReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.LogUserPageVisitAction logUserPageVisitAction = _mapper.Map<X.LogUserPageVisitAction>(request);
            //logUserPageVisitAction.RowNo = maxRowNo + 1;

            await _logUserPageVisitActionWriteRepository.AddAsync(logUserPageVisitAction);
            await _logUserPageVisitActionWriteRepository.SaveAsync();

            X.LogUserPageVisitAction savedLogUserPageVisitAction = await _logUserPageVisitActionReadRepository.GetAsync(predicate: x => x.Gid == logUserPageVisitAction.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidLogUserPageVisitActionResponse obj = _mapper.Map<GetByGidLogUserPageVisitActionResponse>(savedLogUserPageVisitAction);
            return new()
            {
                Title = LogUserPageVisitActionsBusinessMessages.ProcessCompleted,
                Message = LogUserPageVisitActionsBusinessMessages.SuccessCreatedLogUserPageVisitActionMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}