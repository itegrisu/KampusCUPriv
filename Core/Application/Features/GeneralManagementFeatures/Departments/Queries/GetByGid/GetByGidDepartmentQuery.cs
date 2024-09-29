using Application.Features.GeneralManagementFeatures.Departments.Rules;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid
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
                X.Department? department = await _departmentReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.CoAdminFK).Include(x => x.MainAdminFK));

                await _departmentBusinessRules.DepartmentShouldExistWhenSelected(department);

                GetByGidDepartmentResponse response = _mapper.Map<GetByGidDepartmentResponse>(department);
                return response;
            }
        }
    }
}