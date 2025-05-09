using Application.Features.DefinitionFeatures.Departments.Constants;
using Application.Features.DefinitionFeatures.Departments.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Departments.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;
using Application.Abstractions.Redis;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Create;

public class CreateDepartmentCommand : IRequest<CreatedDepartmentResponse>
{
    public string Name { get; set; }

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreatedDepartmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentWriteRepository _departmentWriteRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly DepartmentBusinessRules _departmentBusinessRules;
        private readonly IRedisCacheService _redisCacheService;

        public CreateDepartmentCommandHandler(IMapper mapper, IDepartmentWriteRepository departmentWriteRepository,
                                         DepartmentBusinessRules departmentBusinessRules, IDepartmentReadRepository departmentReadRepository, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _departmentWriteRepository = departmentWriteRepository;
            _departmentBusinessRules = departmentBusinessRules;
            _departmentReadRepository = departmentReadRepository;
            _redisCacheService = redisCacheService;
        }

        public async Task<CreatedDepartmentResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _departmentReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Department department = _mapper.Map<X.Department>(request);
            //department.RowNo = maxRowNo + 1;

            await _departmentWriteRepository.AddAsync(department);
            await _departmentWriteRepository.SaveAsync();

            X.Department savedDepartment = await _departmentReadRepository.GetAsync(predicate: x => x.Gid == department.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            await _redisCacheService.RemoveByPattern("Departments_");

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