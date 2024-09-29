using Application.Features.GeneralManagementFeatures.Departments.Constants;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Departments.Rules;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Update;

public class UpdateDepartmentCommand : IRequest<UpdatedDepartmentResponse>
{
    public Guid Gid { get; set; }
    public Guid GidMainAdminFK { get; set; }
    public Guid GidCoAdminFK { get; set; }
    public string Name { get; set; }
    public string? Detail { get; set; }



    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, UpdatedDepartmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentWriteRepository _departmentWriteRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly DepartmentBusinessRules _departmentBusinessRules;

        public UpdateDepartmentCommandHandler(IMapper mapper, IDepartmentWriteRepository departmentWriteRepository,
                                         DepartmentBusinessRules departmentBusinessRules, IDepartmentReadRepository departmentReadRepository)
        {
            _mapper = mapper;
            _departmentWriteRepository = departmentWriteRepository;
            _departmentBusinessRules = departmentBusinessRules;
            _departmentReadRepository = departmentReadRepository;
        }

        public async Task<UpdatedDepartmentResponse> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            X.Department? department = await _departmentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _departmentBusinessRules.DepartmentShouldExistWhenSelected(department);
            await _departmentBusinessRules.UserShouldExistWhenSelected(request.GidMainAdminFK);
            await _departmentBusinessRules.UserShouldExistWhenSelected(request.GidCoAdminFK);
            await _departmentBusinessRules.CheckDepartmentName(request.Name, request.Gid);


            department = _mapper.Map(request, department);

            _departmentWriteRepository.Update(department!);
            await _departmentWriteRepository.SaveAsync();
            GetByGidDepartmentResponse obj = _mapper.Map<GetByGidDepartmentResponse>(department);

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