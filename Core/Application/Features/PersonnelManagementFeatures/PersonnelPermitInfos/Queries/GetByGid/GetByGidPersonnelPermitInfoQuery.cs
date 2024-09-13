using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid
{
    public class GetByGidPersonnelPermitInfoQuery : IRequest<GetByGidPersonnelPermitInfoResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelPermitInfoQueryHandler : IRequestHandler<GetByGidPersonnelPermitInfoQuery, GetByGidPersonnelPermitInfoResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelPermitInfoReadRepository _personnelPermitInfoReadRepository;
            private readonly PersonnelPermitInfoBusinessRules _personnelPermitInfoBusinessRules;

            public GetByGidPersonnelPermitInfoQueryHandler(IMapper mapper, IPersonnelPermitInfoReadRepository personnelPermitInfoReadRepository, PersonnelPermitInfoBusinessRules personnelPermitInfoBusinessRules)
            {
                _mapper = mapper;
                _personnelPermitInfoReadRepository = personnelPermitInfoReadRepository;
                _personnelPermitInfoBusinessRules = personnelPermitInfoBusinessRules;
            }

            public async Task<GetByGidPersonnelPermitInfoResponse> Handle(GetByGidPersonnelPermitInfoQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelPermitInfo? personnelPermitInfo = await _personnelPermitInfoReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.PermitTypeFK));

                await _personnelPermitInfoBusinessRules.PersonnelPermitInfoShouldExistWhenSelected(personnelPermitInfo);

                GetByGidPersonnelPermitInfoResponse response = _mapper.Map<GetByGidPersonnelPermitInfoResponse>(personnelPermitInfo);
                return response;
            }
        }
    }
}