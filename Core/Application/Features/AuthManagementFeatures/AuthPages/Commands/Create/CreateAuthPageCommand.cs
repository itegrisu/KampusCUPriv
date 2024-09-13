using Application.Features.AuthManagementFeatures.AuthPages.Constants;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthPages.Rules;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Create;

public class CreateAuthPageCommand : IRequest<CreatedAuthPageResponse>
{
    public string PageName { get; set; }
    public string RedirectName { get; set; }
    public string PhysicalFilePath { get; set; }
    public string? MenuLink { get; set; }
    public string? PathForAuthCheck { get; set; }
    public bool IsShowMenu { get; set; }
    public string? HelpFileName { get; set; }

    public class CreateAuthPageCommandHandler : IRequestHandler<CreateAuthPageCommand, CreatedAuthPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthPageWriteRepository _authPageWriteRepository;
        private readonly IAuthPageReadRepository _authPageReadRepository;
        private readonly AuthPageBusinessRules _authPageBusinessRules;

        public CreateAuthPageCommandHandler(IMapper mapper, IAuthPageWriteRepository authPageWriteRepository,
                                         AuthPageBusinessRules authPageBusinessRules, IAuthPageReadRepository authPageReadRepository)
        {
            _mapper = mapper;
            _authPageWriteRepository = authPageWriteRepository;
            _authPageBusinessRules = authPageBusinessRules;
            _authPageReadRepository = authPageReadRepository;
        }

        public async Task<CreatedAuthPageResponse> Handle(CreateAuthPageCommand request, CancellationToken cancellationToken)
        {
            List<AuthPage> authPages = await _authPageReadRepository.GetAll().ToListAsync();
            int maxRowNo = 0;
            if (authPages.Count > 0)
            {
                maxRowNo = await _authPageReadRepository.GetAll().MaxAsync(r => r.RowNo);
            }
            AuthPage authPage = _mapper.Map<AuthPage>(request);
            authPage.RowNo = maxRowNo + 1;

            await _authPageWriteRepository.AddAsync(authPage);
            await _authPageWriteRepository.SaveAsync();

            GetByGidAuthPageResponse obj = _mapper.Map<GetByGidAuthPageResponse>(authPage);


            return new()
            {
                Title = AuthPagesBusinessMessages.ProcessCompleted,
                Message = AuthPagesBusinessMessages.SuccessCreatedAuthPageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}