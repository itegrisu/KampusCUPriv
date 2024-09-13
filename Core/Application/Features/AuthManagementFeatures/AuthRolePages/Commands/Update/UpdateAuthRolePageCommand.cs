using Application.Features.AuthManagementFeatures.AuthRolePages.Constants;
using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRolePages.Rules;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Update;

public class UpdateAuthRolePageCommand : IRequest<UpdatedAuthRolePageResponse>
{
    public Guid Gid { get; set; }
    public Guid GidRoleFK { get; set; }
    public Guid GidPageFK { get; set; }

    public class UpdateAuthRolePageCommandHandler : IRequestHandler<UpdateAuthRolePageCommand, UpdatedAuthRolePageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRolePageWriteRepository _authRolePageWriteRepository;
        private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
        private readonly AuthRolePageBusinessRules _authRolePageBusinessRules;

        public UpdateAuthRolePageCommandHandler(IMapper mapper, IAuthRolePageWriteRepository authRolePageWriteRepository, IAuthRolePageReadRepository authRolePageReadRepository, AuthRolePageBusinessRules authRolePageBusinessRules)
        {
            _mapper = mapper;
            _authRolePageWriteRepository = authRolePageWriteRepository;
            _authRolePageReadRepository = authRolePageReadRepository;
            _authRolePageBusinessRules = authRolePageBusinessRules;
        }

        public async Task<UpdatedAuthRolePageResponse> Handle(UpdateAuthRolePageCommand request, CancellationToken cancellationToken)
        {
            await _authRolePageBusinessRules.AuthRolePageShouldExistWhenSelected(request.Gid); // Role Page'in olup olmadýðýný kontrol eder.
            AuthRolePage? authRolePage = await _authRolePageReadRepository.GetAsync(predicate: arp => arp.Gid == request.Gid, cancellationToken: cancellationToken);
            
            authRolePage = _mapper.Map(request, authRolePage);

            _authRolePageWriteRepository.Update(authRolePage!);
            await _authRolePageWriteRepository.SaveAsync();

            AuthRolePage? updatedAuthRolePage = await _authRolePageReadRepository.GetAsync(predicate: arp => arp.Gid == authRolePage.Gid,
               cancellationToken: cancellationToken,
               include: source => source.Include(arp => arp.AuthRoleFK).Include(arp => arp.AuthPageFK));

            GetByGidAuthRolePageResponse obj = _mapper.Map<GetByGidAuthRolePageResponse>(updatedAuthRolePage);

            return new()
            {
                Title = AuthRolePagesBusinessMessages.ProcessCompleted,
                Message = AuthRolePagesBusinessMessages.SuccessUpdatedAuthRolePageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}