using Application.Features.AccommodationManagementFeatures.Reservations.Constants;
using Application.Features.AccommodationManagementFeatures.Reservations.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationRepo;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Commands.Delete;

public class DeleteReservationCommand : IRequest<DeletedReservationResponse>
{
	public Guid Gid { get; set; }

    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, DeletedReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;

        public DeleteReservationCommandHandler(IMapper mapper, IReservationReadRepository reservationReadRepository,
                                         ReservationBusinessRules reservationBusinessRules, IReservationWriteRepository reservationWriteRepository)
        {
            _mapper = mapper;
            _reservationReadRepository = reservationReadRepository;
            _reservationBusinessRules = reservationBusinessRules;
            _reservationWriteRepository = reservationWriteRepository;
        }

        public async Task<DeletedReservationResponse> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            X.Reservation? reservation = await _reservationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _reservationBusinessRules.ReservationShouldExistWhenSelected(reservation);
            reservation.DataState = Core.Enum.DataState.Deleted;

            _reservationWriteRepository.Update(reservation);
            await _reservationWriteRepository.SaveAsync();

            return new()
            {
                Title = ReservationsBusinessMessages.ProcessCompleted,
                Message = ReservationsBusinessMessages.SuccessDeletedReservationMessage,
                IsValid = true
            };
        }
    }
}