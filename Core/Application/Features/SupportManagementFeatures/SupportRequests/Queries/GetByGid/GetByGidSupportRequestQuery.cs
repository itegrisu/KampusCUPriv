using AutoMapper;
using MediatR;
using X = Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using Application.Features.SupportManagementFeatures.SupportRequests.Rules;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid
{
    public class GetByGidSupportRequestQuery : IRequest<GetByGidSupportRequestResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSupportRequestQueryHandler : IRequestHandler<GetByGidSupportRequestQuery, GetByGidSupportRequestResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISupportRequestReadRepository _supportRequestReadRepository;
            private readonly SupportRequestBusinessRules _supportRequestBusinessRules;

            public GetByGidSupportRequestQueryHandler(IMapper mapper, ISupportRequestReadRepository supportRequestReadRepository, SupportRequestBusinessRules supportRequestBusinessRules)
            {
                _mapper = mapper;
                _supportRequestReadRepository = supportRequestReadRepository;
                _supportRequestBusinessRules = supportRequestBusinessRules;
            }

            public async Task<GetByGidSupportRequestResponse> Handle(GetByGidSupportRequestQuery request, CancellationToken cancellationToken)
            {
                X.SupportRequest? supportRequest = await _supportRequestReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                    include: i=>i.Include(i=>i.UserFK)
                    );
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _supportRequestBusinessRules.SupportRequestShouldExistWhenSelected(supportRequest);

                GetByGidSupportRequestResponse response = _mapper.Map<GetByGidSupportRequestResponse>(supportRequest);
                return response;
            }
        }
    }
}