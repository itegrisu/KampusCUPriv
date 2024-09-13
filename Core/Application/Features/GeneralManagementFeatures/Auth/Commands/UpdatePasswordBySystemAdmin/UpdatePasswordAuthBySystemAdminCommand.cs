using Application.Abstractions.Auth;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.UpdatePasswordBySystemAdmin
{
    public class UpdatePasswordAuthBySystemAdminCommand : IRequest<UpdatePasswordAuthBySystemAdminResponse>
    {
        public Guid Gid { get; set; } // this is the system admin's gid
        public Guid GidUser { get; set; }  // this is the user's gid
        public string NewPassword { get; set; }
        public class UpdatePasswordAuthBySystemAdminCommandHandler : IRequestHandler<UpdatePasswordAuthBySystemAdminCommand, UpdatePasswordAuthBySystemAdminResponse>
        {
            private readonly IAuthService _authService;

            public UpdatePasswordAuthBySystemAdminCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<UpdatePasswordAuthBySystemAdminResponse> Handle(UpdatePasswordAuthBySystemAdminCommand request, CancellationToken cancellationToken)
            {
                UpdatePasswordAuthBySystemAdminResponse response = await _authService.UpdatePasswordBySystemAdmin(request);
                return response;
            }
        }

    }
}
