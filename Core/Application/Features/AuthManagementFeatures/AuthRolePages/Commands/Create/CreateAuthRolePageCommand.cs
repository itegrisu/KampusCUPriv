using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRolePages.Constants;
using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRolePages.Rules;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Create;

public class CreateAuthRolePageCommand : IRequest<CreatedAuthRolePageResponse>
{
    public Guid GidRoleFK { get; set; }
    public Guid GidPageFK { get; set; }

    public class CreateAuthRolePageCommandHandler : IRequestHandler<CreateAuthRolePageCommand, CreatedAuthRolePageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRolePageWriteRepository _authRolePageWriteRepository;
        private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
        private readonly AuthRolePageBusinessRules _authRolePageBusinessRules;

        public CreateAuthRolePageCommandHandler(IMapper mapper, IAuthRolePageWriteRepository authRolePageWriteRepository,
                                         AuthRolePageBusinessRules authRolePageBusinessRules, IAuthRolePageReadRepository authRolePageReadRepository)
        {
            _mapper = mapper;
            _authRolePageWriteRepository = authRolePageWriteRepository;
            _authRolePageBusinessRules = authRolePageBusinessRules;
            _authRolePageReadRepository = authRolePageReadRepository;
        }

        public async Task<CreatedAuthRolePageResponse> Handle(CreateAuthRolePageCommand request, CancellationToken cancellationToken)
        {

            await _authRolePageBusinessRules.AuthRoleShouldExistWhenSelected(request.GidRoleFK); // Role'nin olup olmadýðýný kontrol eder.
            await _authRolePageBusinessRules.AuthPageShouldExistWhenSelected(request.GidPageFK); // Page'in olup olmadýðýný kontrol eder.
            await _authRolePageBusinessRules.AuthPageHasBeenAddedBefore(request.GidRoleFK, request.GidPageFK); // Page'in daha önce eklenip eklenmediðini kontrol eder.

            List<AuthRolePage> authRolePages = await _authRolePageReadRepository.GetAll().Where(x => x.GidRoleFK == request.GidRoleFK).ToListAsync();
            int maxRowNo = 0;
            if (authRolePages.Count > 0)
            {
                maxRowNo = await _authRolePageReadRepository.GetAll().MaxAsync(r => r.RowNo);
            }
            AuthRolePage authRolePage = _mapper.Map<AuthRolePage>(request);
            authRolePage.RowNo = maxRowNo + 1;

            await _authRolePageWriteRepository.AddAsync(authRolePage);
            await _authRolePageWriteRepository.SaveAsync();

            AuthRolePage? savedAuthRolePage = await _authRolePageReadRepository.GetAsync(predicate: arp => arp.Gid == authRolePage.Gid, 
                cancellationToken: cancellationToken,
                include: source => source.Include(arp => arp.AuthRoleFK).Include(arp => arp.AuthPageFK));

            GetByGidAuthRolePageResponse obj = _mapper.Map<GetByGidAuthRolePageResponse>(savedAuthRolePage);
            return new()
            {
                Title = AuthRolePagesBusinessMessages.ProcessCompleted,
                Message = AuthRolePagesBusinessMessages.SuccessCreatedAuthRolePageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}