using Application.Features.AuthManagementFeatures.AuthRolePages.Constants;
using Application.Features.AuthManagementFeatures.AuthRolePages.Rules;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Delete;

public class DeleteAuthRolePageCommand : IRequest<DeletedAuthRolePageResponse>
{
    public Guid Gid { get; set; }

    public class DeleteAuthRolePageCommandHandler : IRequestHandler<DeleteAuthRolePageCommand, DeletedAuthRolePageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRolePageWriteRepository _authRolePageWriteRepository;
        private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
        private readonly AuthRolePageBusinessRules _authRolePageBusinessRules;

        public DeleteAuthRolePageCommandHandler(IMapper mapper,
                                         AuthRolePageBusinessRules authRolePageBusinessRules,
                                         IAuthRolePageWriteRepository authRolePageWriteRepository,
                                         IAuthRolePageReadRepository authRolePageReadRepository)
        {
            _mapper = mapper;
            _authRolePageBusinessRules = authRolePageBusinessRules;
            _authRolePageWriteRepository = authRolePageWriteRepository;
            _authRolePageReadRepository = authRolePageReadRepository;
        }

        public async Task<DeletedAuthRolePageResponse> Handle(DeleteAuthRolePageCommand request, CancellationToken cancellationToken)
        {
            await _authRolePageBusinessRules.AuthRolePageShouldExistWhenSelected(request.Gid);// Role Page'in olup olmadýðýný kontrol eder.
            AuthRolePage? authRolePage = await _authRolePageReadRepository.GetAsync(predicate: arp => arp.Gid == request.Gid, cancellationToken: cancellationToken);
            

            authRolePage.DataState = Core.Enum.DataState.Deleted;
            _authRolePageWriteRepository.Update(authRolePage);
            await _authRolePageWriteRepository.SaveAsync();

            return new()
            {
                Title = AuthRolePagesBusinessMessages.ProcessCompleted,
                Message = AuthRolePagesBusinessMessages.SuccessDeletedAuthRolePageMessage,
                IsValid = true
            };
        }
    }
}