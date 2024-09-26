using AutoMapper;
using MediatR;
using X = Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Rules;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid
{
    public class GetByGidSupportMessageDetailQuery : IRequest<GetByGidSupportMessageDetailResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSupportMessageDetailQueryHandler : IRequestHandler<GetByGidSupportMessageDetailQuery, GetByGidSupportMessageDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISupportMessageDetailReadRepository _supportMessageDetailReadRepository;
            private readonly SupportMessageDetailBusinessRules _supportMessageDetailBusinessRules;

            public GetByGidSupportMessageDetailQueryHandler(IMapper mapper, ISupportMessageDetailReadRepository supportMessageDetailReadRepository, SupportMessageDetailBusinessRules supportMessageDetailBusinessRules)
            {
                _mapper = mapper;
                _supportMessageDetailReadRepository = supportMessageDetailReadRepository;
                _supportMessageDetailBusinessRules = supportMessageDetailBusinessRules;
            }

            public async Task<GetByGidSupportMessageDetailResponse> Handle(GetByGidSupportMessageDetailQuery request, CancellationToken cancellationToken)
            {
                X.SupportMessageDetail? supportMessageDetail = await _supportMessageDetailReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid,      
                    include: i => i.Include(i => i.SupportMessageFK).Include(i => i.UserFK),
                    cancellationToken: cancellationToken);
                  
                await _supportMessageDetailBusinessRules.SupportMessageDetailShouldExistWhenSelected(supportMessageDetail);

                GetByGidSupportMessageDetailResponse response = _mapper.Map<GetByGidSupportMessageDetailResponse>(supportMessageDetail);
                return response;
            }
        }
    }
}