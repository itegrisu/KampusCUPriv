using Application.Features.AuthManagementFeatures.AuthRolePages.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AuthManagements;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.UpdateRowNo
{
    public class UpdateRowNoAuthRolePageCommand : IRequest<UpdateRowNoAuthRolePageResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoAuthRolePageCommandHandler : IRequestHandler<UpdateRowNoAuthRolePageCommand, UpdateRowNoAuthRolePageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAuthRolePageWriteRepository _authRolePageWriteRepository;
            private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
            private readonly AuthRolePageBusinessRules _authRolePageBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoAuthRolePageResponse, X.AuthRolePage> _updateRowNoHelper;

            public UpdateRowNoAuthRolePageCommandHandler(IMapper mapper, IAuthRolePageWriteRepository authRolePageWriteRepository,
                                             AuthRolePageBusinessRules authRolePageBusinessRules, IAuthRolePageReadRepository authRolePageReadRepository, UpdateRowNoHelper<UpdateRowNoAuthRolePageResponse, X.AuthRolePage> updateRowNoHelper)
            {
                _mapper = mapper;
                _authRolePageWriteRepository = authRolePageWriteRepository;
                _authRolePageBusinessRules = authRolePageBusinessRules;
                _authRolePageReadRepository = authRolePageReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoAuthRolePageResponse> Handle(UpdateRowNoAuthRolePageCommand request, CancellationToken cancellationToken)
            {
                var authRolePage = await _authRolePageReadRepository.GetAsync(x=>x.Gid == request.Gid);
                List<X.AuthRolePage> lst = _authRolePageReadRepository.GetAll().Where(x => x.GidRoleFK == authRolePage.GidRoleFK).OrderBy(a => a.RowNo).ToList();

                X.AuthRolePage select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoAuthRolePageResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _authRolePageWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}