using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid
{
    public class GetByGidPersonnelResidenceInfoQuery : IRequest<GetByGidPersonnelResidenceInfoResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelResidenceInfoQueryHandler : IRequestHandler<GetByGidPersonnelResidenceInfoQuery, GetByGidPersonnelResidenceInfoResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelResidenceInfoReadRepository _personnelResidenceInfoReadRepository;
            private readonly PersonnelResidenceInfoBusinessRules _personnelResidenceInfoBusinessRules;

            public GetByGidPersonnelResidenceInfoQueryHandler(IMapper mapper, IPersonnelResidenceInfoReadRepository personnelResidenceInfoReadRepository, PersonnelResidenceInfoBusinessRules personnelResidenceInfoBusinessRules)
            {
                _mapper = mapper;
                _personnelResidenceInfoReadRepository = personnelResidenceInfoReadRepository;
                _personnelResidenceInfoBusinessRules = personnelResidenceInfoBusinessRules;
            }

            public async Task<GetByGidPersonnelResidenceInfoResponse> Handle(GetByGidPersonnelResidenceInfoQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelResidenceInfo? personnelResidenceInfo = await _personnelResidenceInfoReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK));

                await _personnelResidenceInfoBusinessRules.PersonnelResidenceInfoShouldExistWhenSelected(personnelResidenceInfo);

                GetByGidPersonnelResidenceInfoResponse response = _mapper.Map<GetByGidPersonnelResidenceInfoResponse>(personnelResidenceInfo);
                return response;
            }
        }
    }
}