using Application.Features.GeneralManagementFeatures.Departments.Constants;
using Application.Features.GeneralManagementFeatures.Departments.Rules;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Delete;

public class DeleteDepartmentCommand : IRequest<DeletedDepartmentResponse>
{
	public Guid Gid { get; set; }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, DeletedDepartmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly IDepartmentWriteRepository _departmentWriteRepository;
        private readonly DepartmentBusinessRules _departmentBusinessRules;

        public DeleteDepartmentCommandHandler(IMapper mapper, IDepartmentReadRepository departmentReadRepository,
                                         DepartmentBusinessRules departmentBusinessRules, IDepartmentWriteRepository departmentWriteRepository)
        {
            _mapper = mapper;
            _departmentReadRepository = departmentReadRepository;
            _departmentBusinessRules = departmentBusinessRules;
            _departmentWriteRepository = departmentWriteRepository;
        }

        public async Task<DeletedDepartmentResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            X.Department? department = await _departmentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _departmentBusinessRules.DepartmentShouldExistWhenSelected(department);
            department.DataState = Core.Enum.DataState.Deleted;

            _departmentWriteRepository.Update(department);
            await _departmentWriteRepository.SaveAsync();

            return new()
            {
                Title = DepartmentsBusinessMessages.ProcessCompleted,
                Message = DepartmentsBusinessMessages.SuccessDeletedDepartmentMessage,
                IsValid = true
            };
        }
    }
}