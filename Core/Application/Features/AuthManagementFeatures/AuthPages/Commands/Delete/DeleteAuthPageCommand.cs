using Application.Features.AuthManagementFeatures.AuthPages.Constants;
using Application.Features.AuthManagementFeatures.AuthPages.Rules;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Delete;

public class DeleteAuthPageCommand : IRequest<DeletedAuthPageResponse>
{
    public Guid Gid { get; set; }

    public class DeleteAuthPageCommandHandler : IRequestHandler<DeleteAuthPageCommand, DeletedAuthPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthPageReadRepository _authPageReadRepository;
        private readonly IAuthPageWriteRepository _authPageWriteRepository;
        private readonly AuthPageBusinessRules _authPageBusinessRules;

        public DeleteAuthPageCommandHandler(IMapper mapper, IAuthPageReadRepository authPageReadRepository,
                                         AuthPageBusinessRules authPageBusinessRules, IAuthPageWriteRepository authPageWriteRepository)
        {
            _mapper = mapper;
            _authPageReadRepository = authPageReadRepository;
            _authPageBusinessRules = authPageBusinessRules;
            _authPageWriteRepository = authPageWriteRepository;
        }

        public async Task<DeletedAuthPageResponse> Handle(DeleteAuthPageCommand request, CancellationToken cancellationToken)
        {
            await _authPageBusinessRules.AuthPageShouldExistWhenSelected(request.Gid);
            AuthPage? authPage = await _authPageReadRepository.GetAsync(predicate: ap => ap.Gid == request.Gid, cancellationToken: cancellationToken);

            authPage.DataState = Core.Enum.DataState.Deleted;
            _authPageWriteRepository.Update(authPage!);
            await _authPageWriteRepository.SaveAsync();



            return new()
            {
                Title = AuthPagesBusinessMessages.ProcessCompleted,
                Message = AuthPagesBusinessMessages.SuccessDeletedAuthPageMessage,
                IsValid = true,
            };
        }
    }
}