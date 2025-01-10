using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.DefinitionFeatures.Departments.Rules;
using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;

namespace Application.Features.DefinitionFeatures.Departments.Queries.GetByGid
{
    public class GetByGidDepartmentQuery : IRequest<GetByGidDepartmentResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidDepartmentQueryHandler : IRequestHandler<GetByGidDepartmentQuery, GetByGidDepartmentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDepartmentReadRepository _departmentReadRepository;
            private readonly DepartmentBusinessRules _departmentBusinessRules;

            public GetByGidDepartmentQueryHandler(IMapper mapper, IDepartmentReadRepository departmentReadRepository, DepartmentBusinessRules departmentBusinessRules)
            {
                _mapper = mapper;
                _departmentReadRepository = departmentReadRepository;
                _departmentBusinessRules = departmentBusinessRules;
            }

            public async Task<GetByGidDepartmentResponse> Handle(GetByGidDepartmentQuery request, CancellationToken cancellationToken)
            {
                X.Department? department = await _departmentReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _departmentBusinessRules.DepartmentShouldExistWhenSelected(department);

                GetByGidDepartmentResponse response = _mapper.Map<GetByGidDepartmentResponse>(department);
                return response;
            }
        }
    }
}