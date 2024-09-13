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
    public Guid GidAsilYoneticiFK { get; set; }
    public Guid GidYedekYoneticiFK { get; set; }
    public string DepartmanAdi { get; set; }
    public string? Detay { get; set; }



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
            await _departmentBusinessRules.UserShouldExistWhenSelected(request.GidAsilYoneticiFK);
            await _departmentBusinessRules.UserShouldExistWhenSelected(request.GidYedekYoneticiFK);
            await _departmentBusinessRules.CheckDepartmentName(request.DepartmanAdi);

            X.Department department = _mapper.Map<X.Department>(request);

            await _departmentWriteRepository.AddAsync(department);
            await _departmentWriteRepository.SaveAsync();

            X.Department savedDepartment = await _departmentReadRepository.GetAsync(predicate: x => x.Gid == department.Gid, include: x => x.Include(x => x.AsilYoneticFK).Include(x => x.YedekYoneticiFK));



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