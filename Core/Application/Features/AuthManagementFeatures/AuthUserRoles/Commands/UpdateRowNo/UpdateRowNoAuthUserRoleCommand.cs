using Application.Features.AuthManagementFeatures.AuthUserRoles.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AuthManagements;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.UpdateRowNo
{
    public class UpdateRowNoAuthUserRoleCommand : IRequest<UpdateRowNoAuthUserRoleResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoAuthUserRoleCommandHandler : IRequestHandler<UpdateRowNoAuthUserRoleCommand, UpdateRowNoAuthUserRoleResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAuthUserRoleWriteRepository _authUserRoleWriteRepository;
            private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
            private readonly AuthUserRoleBusinessRules _authUserRoleBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoAuthUserRoleResponse, X.AuthUserRole> _updateRowNoHelper;

            public UpdateRowNoAuthUserRoleCommandHandler(IMapper mapper, IAuthUserRoleWriteRepository authUserRoleWriteRepository,
                                             AuthUserRoleBusinessRules authUserRoleBusinessRules, IAuthUserRoleReadRepository authUserRoleReadRepository, UpdateRowNoHelper<UpdateRowNoAuthUserRoleResponse, X.AuthUserRole> updateRowNoHelper)
            {
                _mapper = mapper;
                _authUserRoleWriteRepository = authUserRoleWriteRepository;
                _authUserRoleBusinessRules = authUserRoleBusinessRules;
                _authUserRoleReadRepository = authUserRoleReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoAuthUserRoleResponse> Handle(UpdateRowNoAuthUserRoleCommand request, CancellationToken cancellationToken)
            {
                var authUserRole = await _authUserRoleReadRepository.GetAsync(x => x.Gid == request.Gid);
                List<X.AuthUserRole> lst;
                if (authUserRole.GidRoleFK == null)
                    lst = _authUserRoleReadRepository.GetAll().Where(x => x.GidUserFK == authUserRole.GidUserFK && x.GidRoleFK == null).OrderBy(a => a.RowNo).ToList();
                else
                    lst = _authUserRoleReadRepository.GetAll().Where(x => x.GidUserFK == authUserRole.GidUserFK && x.GidPageFK == null).OrderBy(a => a.RowNo).ToList();

                X.AuthUserRole select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoAuthUserRoleResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _authUserRoleWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}