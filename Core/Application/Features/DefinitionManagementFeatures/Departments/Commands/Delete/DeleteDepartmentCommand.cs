using Application.Features.DefinitionFeatures.Departments.Constants;
using Application.Features.DefinitionFeatures.Departments.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;
using Application.Abstractions.Redis;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Delete;

public class DeleteDepartmentCommand : IRequest<DeletedDepartmentResponse>
{
	public Guid Gid { get; set; }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, DeletedDepartmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly IDepartmentWriteRepository _departmentWriteRepository;
        private readonly DepartmentBusinessRules _departmentBusinessRules;
        private readonly IRedisCacheService _redisCacheService;

        public DeleteDepartmentCommandHandler(IMapper mapper, IDepartmentReadRepository departmentReadRepository,
                                         DepartmentBusinessRules departmentBusinessRules, IDepartmentWriteRepository departmentWriteRepository, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _departmentReadRepository = departmentReadRepository;
            _departmentBusinessRules = departmentBusinessRules;
            _departmentWriteRepository = departmentWriteRepository;
            _redisCacheService = redisCacheService;
        }

        public async Task<DeletedDepartmentResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            X.Department? department = await _departmentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _departmentBusinessRules.DepartmentShouldExistWhenSelected(department);
            department.DataState = Core.Enum.DataState.Deleted;

            _departmentWriteRepository.Update(department);
            await _departmentWriteRepository.SaveAsync();

            await _redisCacheService.RemoveByPattern("Departments_");

            return new()
            {
                Title = DepartmentsBusinessMessages.ProcessCompleted,
                Message = DepartmentsBusinessMessages.SuccessDeletedDepartmentMessage,
                IsValid = true
            };
        }
    }
}