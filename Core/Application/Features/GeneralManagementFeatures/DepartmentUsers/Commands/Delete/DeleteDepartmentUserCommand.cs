using Application.Features.GeneralManagementFeatures.DepartmentUsers.Constants;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Rules;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Delete;

public class DeleteDepartmentUserCommand : IRequest<DeletedDepartmentUserResponse>
{
    public Guid Gid { get; set; }

    public class DeleteDepartmentUserCommandHandler : IRequestHandler<DeleteDepartmentUserCommand, DeletedDepartmentUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
        private readonly IDepartmentUserWriteRepository _departmentUserWriteRepository;
        private readonly DepartmentUserBusinessRules _departmentUserBusinessRules;

        public DeleteDepartmentUserCommandHandler(IMapper mapper, IDepartmentUserReadRepository departmentUserReadRepository,
                                         DepartmentUserBusinessRules departmentUserBusinessRules, IDepartmentUserWriteRepository departmentUserWriteRepository)
        {
            _mapper = mapper;
            _departmentUserReadRepository = departmentUserReadRepository;
            _departmentUserBusinessRules = departmentUserBusinessRules;
            _departmentUserWriteRepository = departmentUserWriteRepository;
        }

        public async Task<DeletedDepartmentUserResponse> Handle(DeleteDepartmentUserCommand request, CancellationToken cancellationToken)
        {
            var ex = _departmentUserBusinessRules.CheckIfUserCanBeDeleted(request.Gid);

            if (ex.IsFaulted)
            {
                return new()
                {
                    Title = DepartmentUsersBusinessMessages.SectionName,
                    Message = DepartmentUsersBusinessMessages.HasAdminUser,
                    IsValid = false
                };
            }

            X.DepartmentUser? departmentUser = await _departmentUserReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _departmentUserBusinessRules.DepartmentUserShouldExistWhenSelected(departmentUser);
            departmentUser.DataState = Core.Enum.DataState.Deleted;

            _departmentUserWriteRepository.Update(departmentUser);
            await _departmentUserWriteRepository.SaveAsync();

            return new()
            {
                Title = DepartmentUsersBusinessMessages.ProcessCompleted,
                Message = DepartmentUsersBusinessMessages.SuccessDeletedDepartmentUserMessage,
                IsValid = true
            };
        }
    }
}