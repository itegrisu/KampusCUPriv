using Application.Features.AuthManagementFeatures.AuthPages.Constants;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthPages.Rules;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Update;

public class UpdateAuthPageCommand : IRequest<UpdatedAuthPageResponse>
{
    public Guid Gid { get; set; }
    public string PageName { get; set; }
    public string RedirectName { get; set; }
    public string PhysicalFilePath { get; set; }
    public string? MenuLink { get; set; }
    public string? PathForAuthCheck { get; set; }
    public bool IsShowMenu { get; set; }
    public string? HelpFileName { get; set; }

    public class UpdateAuthPageCommandHandler : IRequestHandler<UpdateAuthPageCommand, UpdatedAuthPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthPageWriteRepository _authPageWriteRepository;
        private readonly IAuthPageReadRepository _authPageReadRepository;
        private readonly AuthPageBusinessRules _authPageBusinessRules;

        public UpdateAuthPageCommandHandler(IMapper mapper, IAuthPageWriteRepository authPageWriteRepository,
                                         AuthPageBusinessRules authPageBusinessRules, IAuthPageReadRepository authPageReadRepository)
        {
            _mapper = mapper;
            _authPageWriteRepository = authPageWriteRepository;
            _authPageBusinessRules = authPageBusinessRules;
            _authPageReadRepository = authPageReadRepository;
        }

        public async Task<UpdatedAuthPageResponse> Handle(UpdateAuthPageCommand request, CancellationToken cancellationToken)
        {
            await _authPageBusinessRules.AuthPageShouldExistWhenSelected(request.Gid);
            AuthPage? authPage = await _authPageReadRepository.GetAsync(predicate: ap => ap.Gid == request.Gid, cancellationToken: cancellationToken);
          
            authPage = _mapper.Map(request, authPage);

            _authPageWriteRepository.Update(authPage!);
            await _authPageWriteRepository.SaveAsync();
            GetByGidAuthPageResponse obj = _mapper.Map<GetByGidAuthPageResponse>(authPage);

            return new()
            {
                Title = AuthPagesBusinessMessages.ProcessCompleted,
                IsValid = true,
                Message = AuthPagesBusinessMessages.SuccessUpdatedAuthPageMessage,
                Obj=obj
            };
        }
    }
}