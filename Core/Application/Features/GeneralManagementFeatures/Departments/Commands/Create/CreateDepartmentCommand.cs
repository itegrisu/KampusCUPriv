using Application.Features.GeneralManagementFeatures.Departments.Constants;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Departments.Rules;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Create;

public class CreateDepartmentCommand : IRequest<CreatedDepartmentResponse>
{
    public Guid GidMainAdminFK { get; set; }
    public Guid? GidCoAdminFK { get; set; }
    public string Name { get; set; }
    public string? Details { get; set; }




    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreatedDepartmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentWriteRepository _departmentWriteRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly DepartmentBusinessRules _departmentBusinessRules;

        public CreateDepartmentCommandHandler(IMapper mapper, IDepartmentWriteRepository departmentWriteRepository,
                                         DepartmentBusinessRules departmentBusinessRules, IDepartmentReadRepository departmentReadRepository)
        {
            _mapper = mapper;
            _departmentWriteRepository = departmentWriteRepository;
            _departmentBusinessRules = departmentBusinessRules;
            _departmentReadRepository = departmentReadRepository;
        }

        public async Task<CreatedDepartmentResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _departmentBusinessRules.UserShouldExistWhenSelected(request.GidMainAdminFK);
            if (request.GidCoAdminFK != null)
            {
                await _departmentBusinessRules.UserShouldExistWhenSelected(request.GidCoAdminFK);
                await _departmentBusinessRules.CheckMainAdminAndCoAdminSameUser(request.GidMainAdminFK, request.GidCoAdminFK);
            }
            await _departmentBusinessRules.CheckDepartmentName(request.Name);

            X.Department department = _mapper.Map<X.Department>(request);

            await _departmentWriteRepository.AddAsync(department);
            await _departmentWriteRepository.SaveAsync();

            X.Department savedDepartment = await _departmentReadRepository.GetAsync(predicate: x => x.Gid == department.Gid, include: x => x.Include(x => x.MainAdminFK).Include(x => x.CoAdminFK));



            GetByGidDepartmentResponse obj = _mapper.Map<GetByGidDepartmentResponse>(savedDepartment);
            return new()
            {
                Title = DepartmentsBusinessMessages.ProcessCompleted,
                Message = DepartmentsBusinessMessages.SuccessCreatedDepartmentMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}