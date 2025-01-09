using Application.Abstractions.Auth;
using Application.Features.GeneralManagementFeatures.Auth.Constants;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Enums;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForWorker
{
    public class LoginForWorkerAuthCommand : IRequest<LoginForWorkerAuthResponse>
    {
        public string Phone { get; set; }
        public string Password { get; set; }

        public class LoginForWorkerAuthCommandHandler : IRequestHandler<LoginForWorkerAuthCommand, LoginForWorkerAuthResponse>
        {
            private readonly IAuthService _authService;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;

            public LoginForWorkerAuthCommandHandler(IAuthService authService, IUserReadRepository userReadRepository, IReservationHotelStaffReadRepository reservationHotelStaffReadRepository)
            {
                _authService = authService;
                _userReadRepository = userReadRepository;
                _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
            }

            public async Task<LoginForWorkerAuthResponse> Handle(LoginForWorkerAuthCommand request, CancellationToken cancellationToken)
            {
                var user = await _reservationHotelStaffReadRepository.GetSingleAsync(x => x.GsmNo == request.Phone);
                if (user != null && user.HotelStaffStatus == EnumHotelStaffStatus.Aktif)
                {
                    LoginForWorkerAuthResponse response = await _authService.LoginForWorker(request);
                    response.IsPartTimeWorker = false;
                    return response;
                }
                return new LoginForWorkerAuthResponse
                {
                    Title = AuthBussinessMessages.LoginFailed,
                    Message = AuthBussinessMessages.NotAuthorisedToLogIn
                };
            }
        }
    }
}
