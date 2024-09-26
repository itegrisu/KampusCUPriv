using AutoMapper;
using MediatR;
using X = Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using Application.Features.SupportManagementFeatures.SupportMessages.Rules;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid
{
    public class GetByGidSupportMessageQuery : IRequest<GetByGidSupportMessageResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSupportMessageQueryHandler : IRequestHandler<GetByGidSupportMessageQuery, GetByGidSupportMessageResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISupportMessageReadRepository _supportMessageReadRepository;
            private readonly SupportMessageBusinessRules _supportMessageBusinessRules;

            public GetByGidSupportMessageQueryHandler(IMapper mapper, ISupportMessageReadRepository supportMessageReadRepository, SupportMessageBusinessRules supportMessageBusinessRules)
            {
                _mapper = mapper;
                _supportMessageReadRepository = supportMessageReadRepository;
                _supportMessageBusinessRules = supportMessageBusinessRules;
            }

            public async Task<GetByGidSupportMessageResponse> Handle(GetByGidSupportMessageQuery request, CancellationToken cancellationToken)
            {
                X.SupportMessage? supportMessage = await _supportMessageReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                    include:i=>i.Include(i=>i.SupportRequestFK).Include(i=>i.UserFK));
                    
                await _supportMessageBusinessRules.SupportMessageShouldExistWhenSelected(supportMessage);

                GetByGidSupportMessageResponse response = _mapper.Map<GetByGidSupportMessageResponse>(supportMessage);
                return response;
            }
        }
    }
}