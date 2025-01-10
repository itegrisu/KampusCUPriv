using Application.Features.DefinitionFeatures.Departments.Constants;
using Application.Features.DefinitionFeatures.Departments.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Departments.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Update;

public class UpdateDepartmentCommand : IRequest<UpdatedDepartmentResponse>
{
    public Guid Gid { get; set; }
    public string Name { get; set; }

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
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _departmentBusinessRules.DepartmentShouldExistWhenSelected(department);
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