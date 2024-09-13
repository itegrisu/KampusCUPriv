using Application.Features.AuthManagementFeatures.AuthPages.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AuthManagements;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.UpdateRowNo
{
    public class UpdateRowNoAuthPageCommand : IRequest<UpdateRowNoAuthPageResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoAuthPageCommandHandler : IRequestHandler<UpdateRowNoAuthPageCommand, UpdateRowNoAuthPageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAuthPageWriteRepository _authPageWriteRepository;
            private readonly IAuthPageReadRepository _authPageReadRepository;
            private readonly AuthPageBusinessRules _authPageBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoAuthPageResponse, X.AuthPage> _updateRowNoHelper;

            public UpdateRowNoAuthPageCommandHandler(IMapper mapper, IAuthPageWriteRepository authPageWriteRepository,
                                             AuthPageBusinessRules authPageBusinessRules, IAuthPageReadRepository authPageReadRepository, UpdateRowNoHelper<UpdateRowNoAuthPageResponse, X.AuthPage> updateRowNoHelper)
            {
                _mapper = mapper;
                _authPageWriteRepository = authPageWriteRepository;
                _authPageBusinessRules = authPageBusinessRules;
                _authPageReadRepository = authPageReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoAuthPageResponse> Handle(UpdateRowNoAuthPageCommand request, CancellationToken cancellationToken)
            {
                List<X.AuthPage> lst = _authPageReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.AuthPage select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoAuthPageResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _authPageWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}