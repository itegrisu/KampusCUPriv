using AutoMapper;
using MediatR;
using X = Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Rules;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid
{
    public class GetByGidDepartmentUserQuery : IRequest<GetByGidDepartmentUserResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidDepartmentUserQueryHandler : IRequestHandler<GetByGidDepartmentUserQuery, GetByGidDepartmentUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
            private readonly DepartmentUserBusinessRules _departmentUserBusinessRules;

            public GetByGidDepartmentUserQueryHandler(IMapper mapper, IDepartmentUserReadRepository departmentUserReadRepository, DepartmentUserBusinessRules departmentUserBusinessRules)
            {
                _mapper = mapper;
                _departmentUserReadRepository = departmentUserReadRepository;
                _departmentUserBusinessRules = departmentUserBusinessRules;
            }

            public async Task<GetByGidDepartmentUserResponse> Handle(GetByGidDepartmentUserQuery request, CancellationToken cancellationToken)
            {
                X.DepartmentUser? departmentUser = await _departmentUserReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _departmentUserBusinessRules.DepartmentUserShouldExistWhenSelected(departmentUser);

                GetByGidDepartmentUserResponse response = _mapper.Map<GetByGidDepartmentUserResponse>(departmentUser);
                return response;
            }
        }
    }
}