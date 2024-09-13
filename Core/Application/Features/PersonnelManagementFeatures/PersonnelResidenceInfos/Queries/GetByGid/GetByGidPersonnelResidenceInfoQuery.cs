using AutoMapper;
using MediatR;
using X = Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;

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
                X.PersonnelResidenceInfo? personnelResidenceInfo = await _personnelResidenceInfoReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _personnelResidenceInfoBusinessRules.PersonnelResidenceInfoShouldExistWhenSelected(personnelResidenceInfo);

                GetByGidPersonnelResidenceInfoResponse response = _mapper.Map<GetByGidPersonnelResidenceInfoResponse>(personnelResidenceInfo);
                return response;
            }
        }
    }
}