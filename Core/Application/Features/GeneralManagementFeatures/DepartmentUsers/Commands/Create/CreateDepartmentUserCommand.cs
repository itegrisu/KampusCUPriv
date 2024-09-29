using Application.Features.GeneralManagementFeatures.DepartmentUsers.Constants;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Rules;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Create;

public class CreateDepartmentUserCommand : IRequest<CreatedDepartmentUserResponse>
{
    public Guid GidDepartmentFK { get; set; }
    public Guid GidPersonnelFK { get; set; }

    public class CreateDepartmentUserCommandHandler : IRequestHandler<CreateDepartmentUserCommand, CreatedDepartmentUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentUserWriteRepository _departmentUserWriteRepository;
        private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
        private readonly DepartmentUserBusinessRules _departmentUserBusinessRules;

        public CreateDepartmentUserCommandHandler(IMapper mapper, IDepartmentUserWriteRepository departmentUserWriteRepository,
                                         DepartmentUserBusinessRules departmentUserBusinessRules, IDepartmentUserReadRepository departmentUserReadRepository)
        {
            _mapper = mapper;
            _departmentUserWriteRepository = departmentUserWriteRepository;
            _departmentUserBusinessRules = departmentUserBusinessRules;
            _departmentUserReadRepository = departmentUserReadRepository;
        }

        public async Task<CreatedDepartmentUserResponse> Handle(CreateDepartmentUserCommand request, CancellationToken cancellationToken)
        {
            await _departmentUserBusinessRules.DepartmantShouldExistWhenSelected(request.GidDepartmentFK);
            await _departmentUserBusinessRules.PersonelShouldExistWhenSelected(request.GidPersonnelFK);

            X.DepartmentUser departmentUser = _mapper.Map<X.DepartmentUser>(request);

            await _departmentUserWriteRepository.AddAsync(departmentUser);
            await _departmentUserWriteRepository.SaveAsync();

            X.DepartmentUser savedDepartmentUser = await _departmentUserReadRepository.GetAsync(predicate: x => x.Gid == departmentUser.Gid, include: x => x.Include(x => x.UserFK).Include(x => x.DepartmentFK));


            GetByGidDepartmentUserResponse obj = _mapper.Map<GetByGidDepartmentUserResponse>(savedDepartmentUser);
            return new()
            {
                Title = DepartmentUsersBusinessMessages.ProcessCompleted,
                Message = DepartmentUsersBusinessMessages.SuccessCreatedDepartmentUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}