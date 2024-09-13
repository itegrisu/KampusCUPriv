using Application.Helpers.UpdateRowNo;
using AutoMapper;
using X = Domain.Entities.AuthManagements;
using MediatR;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using Application.Features.AuthManagementFeatures.AuthRoles.Rules;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.UpdateRowNo
{
    public class UpdateRowNoAuthRoleCommand : IRequest<UpdateRowNoAuthRoleResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoAuthRoleCommandHandler : IRequestHandler<UpdateRowNoAuthRoleCommand, UpdateRowNoAuthRoleResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAuthRoleWriteRepository _authRoleWriteRepository;
            private readonly IAuthRoleReadRepository _authRoleReadRepository;
            private readonly AuthRoleBusinessRules _authRoleBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoAuthRoleResponse, X.AuthRole> _updateRowNoHelper;

            public UpdateRowNoAuthRoleCommandHandler(IMapper mapper, IAuthRoleWriteRepository authRoleWriteRepository,
                                             AuthRoleBusinessRules authRoleBusinessRules, IAuthRoleReadRepository authRoleReadRepository, UpdateRowNoHelper<UpdateRowNoAuthRoleResponse, X.AuthRole> updateRowNoHelper)
            {
                _mapper = mapper;
                _authRoleWriteRepository = authRoleWriteRepository;
                _authRoleBusinessRules = authRoleBusinessRules;
                _authRoleReadRepository = authRoleReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoAuthRoleResponse> Handle(UpdateRowNoAuthRoleCommand request, CancellationToken cancellationToken)
            {
                List<X.AuthRole> lst = _authRoleReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.AuthRole select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoAuthRoleResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _authRoleWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}