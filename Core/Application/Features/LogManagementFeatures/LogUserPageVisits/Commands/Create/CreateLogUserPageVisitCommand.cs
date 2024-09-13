using Application.Features.LogManagementFeatures.LogUserPageVisits.Constants;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Rules;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Helpers;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Create;

public class CreateLogUserPageVisitCommand : IRequest<CreatedLogUserPageVisitResponse>
{
    public Guid GidUserFK { get; set; }
   // public string? IpAddress { get; set; }
    public string PageInfo { get; set; }
    //public string SessionId { get; set; }



    public class CreateLogUserPageVisitCommandHandler : IRequestHandler<CreateLogUserPageVisitCommand, CreatedLogUserPageVisitResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogUserPageVisitWriteRepository _logUserPageVisitWriteRepository;
        private readonly ILogUserPageVisitReadRepository _logUserPageVisitReadRepository;
        private readonly LogUserPageVisitBusinessRules _logUserPageVisitBusinessRules;
        private readonly GetUserInfo _getUserInfo;

        public CreateLogUserPageVisitCommandHandler(IMapper mapper, ILogUserPageVisitWriteRepository logUserPageVisitWriteRepository,
                                         LogUserPageVisitBusinessRules logUserPageVisitBusinessRules, ILogUserPageVisitReadRepository logUserPageVisitReadRepository, GetUserInfo getUserInfo)
        {
            _mapper = mapper;
            _logUserPageVisitWriteRepository = logUserPageVisitWriteRepository;
            _logUserPageVisitBusinessRules = logUserPageVisitBusinessRules;
            _logUserPageVisitReadRepository = logUserPageVisitReadRepository;
            _getUserInfo = getUserInfo;
        }

        public async Task<CreatedLogUserPageVisitResponse> Handle(CreateLogUserPageVisitCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _logUserPageVisitReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.LogUserPageVisit logUserPageVisit = _mapper.Map<X.LogUserPageVisit>(request);
            logUserPageVisit.SessionId = _getUserInfo.GetUserSessionID();
            logUserPageVisit.IpAddress = _getUserInfo.GetUserIpAddress();

            await _logUserPageVisitWriteRepository.AddAsync(logUserPageVisit);
            await _logUserPageVisitWriteRepository.SaveAsync();

            X.LogUserPageVisit savedLogUserPageVisit = await _logUserPageVisitReadRepository.GetAsync(predicate: x => x.Gid == logUserPageVisit.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidLogUserPageVisitResponse obj = _mapper.Map<GetByGidLogUserPageVisitResponse>(savedLogUserPageVisit);
            return new()
            {
                Title = LogUserPageVisitsBusinessMessages.ProcessCompleted,
                Message = LogUserPageVisitsBusinessMessages.SuccessCreatedLogUserPageVisitMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}