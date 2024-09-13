using AutoMapper;
using MediatR;
using X = Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Rules;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid
{
    public class GetByGidPersonnelGraduatedSchoolQuery : IRequest<GetByGidPersonnelGraduatedSchoolResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelGraduatedSchoolQueryHandler : IRequestHandler<GetByGidPersonnelGraduatedSchoolQuery, GetByGidPersonnelGraduatedSchoolResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelGraduatedSchoolReadRepository _personnelGraduatedSchoolReadRepository;
            private readonly PersonnelGraduatedSchoolBusinessRules _personnelGraduatedSchoolBusinessRules;

            public GetByGidPersonnelGraduatedSchoolQueryHandler(IMapper mapper, IPersonnelGraduatedSchoolReadRepository personnelGraduatedSchoolReadRepository, PersonnelGraduatedSchoolBusinessRules personnelGraduatedSchoolBusinessRules)
            {
                _mapper = mapper;
                _personnelGraduatedSchoolReadRepository = personnelGraduatedSchoolReadRepository;
                _personnelGraduatedSchoolBusinessRules = personnelGraduatedSchoolBusinessRules;
            }

            public async Task<GetByGidPersonnelGraduatedSchoolResponse> Handle(GetByGidPersonnelGraduatedSchoolQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelGraduatedSchool? personnelGraduatedSchool = await _personnelGraduatedSchoolReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _personnelGraduatedSchoolBusinessRules.PersonnelGraduatedSchoolShouldExistWhenSelected(personnelGraduatedSchool);

                GetByGidPersonnelGraduatedSchoolResponse response = _mapper.Map<GetByGidPersonnelGraduatedSchoolResponse>(personnelGraduatedSchool);
                return response;
            }
        }
    }
}