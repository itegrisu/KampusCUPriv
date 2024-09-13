using Application.Features.GeneralManagementFeatures.DepartmentUsers.Constants;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Rules;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Update;

public class UpdateDepartmentUserCommand : IRequest<UpdatedDepartmentUserResponse>
{
    public Guid Gid { get; set; }

    public Guid GidDepartmanFK { get; set; }
    public Guid GidPersonelFK { get; set; }

    public class UpdateDepartmentUserCommandHandler : IRequestHandler<UpdateDepartmentUserCommand, UpdatedDepartmentUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentUserWriteRepository _departmentUserWriteRepository;
        private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
        private readonly DepartmentUserBusinessRules _departmentUserBusinessRules;

        public UpdateDepartmentUserCommandHandler(IMapper mapper, IDepartmentUserWriteRepository departmentUserWriteRepository,
                                         DepartmentUserBusinessRules departmentUserBusinessRules, IDepartmentUserReadRepository departmentUserReadRepository)
        {
            _mapper = mapper;
            _departmentUserWriteRepository = departmentUserWriteRepository;
            _departmentUserBusinessRules = departmentUserBusinessRules;
            _departmentUserReadRepository = departmentUserReadRepository;
        }

        public async Task<UpdatedDepartmentUserResponse> Handle(UpdateDepartmentUserCommand request, CancellationToken cancellationToken)
        {
            X.DepartmentUser? departmentUser = await _departmentUserReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _departmentUserBusinessRules.DepartmentUserShouldExistWhenSelected(departmentUser);
            await _departmentUserBusinessRules.DepartmantShouldExistWhenSelected(request.GidDepartmanFK);
            await _departmentUserBusinessRules.PersonelShouldExistWhenSelected(request.GidPersonelFK);
            departmentUser = _mapper.Map(request, departmentUser);

            _departmentUserWriteRepository.Update(departmentUser!);
            await _departmentUserWriteRepository.SaveAsync();
            GetByGidDepartmentUserResponse obj = _mapper.Map<GetByGidDepartmentUserResponse>(departmentUser);

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