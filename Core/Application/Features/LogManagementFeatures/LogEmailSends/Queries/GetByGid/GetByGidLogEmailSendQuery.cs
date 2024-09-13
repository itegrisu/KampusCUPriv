using AutoMapper;
using MediatR;
using X = Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using Application.Features.LogManagementFeatures.LogEmailSends.Rules;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid
{
    public class GetByGidLogEmailSendQuery : IRequest<GetByGidLogEmailSendResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidLogEmailSendQueryHandler : IRequestHandler<GetByGidLogEmailSendQuery, GetByGidLogEmailSendResponse>
        {
            private readonly IMapper _mapper;
            private readonly ILogEmailSendReadRepository _logEmailSendReadRepository;
            private readonly LogEmailSendBusinessRules _logEmailSendBusinessRules;

            public GetByGidLogEmailSendQueryHandler(IMapper mapper, ILogEmailSendReadRepository logEmailSendReadRepository, LogEmailSendBusinessRules logEmailSendBusinessRules)
            {
                _mapper = mapper;
                _logEmailSendReadRepository = logEmailSendReadRepository;
                _logEmailSendBusinessRules = logEmailSendBusinessRules;
            }

            public async Task<GetByGidLogEmailSendResponse> Handle(GetByGidLogEmailSendQuery request, CancellationToken cancellationToken)
            {
                X.LogEmailSend? logEmailSend = await _logEmailSendReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _logEmailSendBusinessRules.LogEmailSendShouldExistWhenSelected(logEmailSend);

                GetByGidLogEmailSendResponse response = _mapper.Map<GetByGidLogEmailSendResponse>(logEmailSend);
                return response;
            }
        }
    }
}