using Application.Features.AccommodationManagementFeatures.ReservationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Delete;

public class DeleteReservationRoomCommand : IRequest<DeletedReservationRoomResponse>
{
	public Guid Gid { get; set; }

    public class DeleteReservationRoomCommandHandler : IRequestHandler<DeleteReservationRoomCommand, DeletedReservationRoomResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
        private readonly IReservationRoomWriteRepository _reservationRoomWriteRepository;
        private readonly ReservationRoomBusinessRules _reservationRoomBusinessRules;

        public DeleteReservationRoomCommandHandler(IMapper mapper, IReservationRoomReadRepository reservationRoomReadRepository,
                                         ReservationRoomBusinessRules reservationRoomBusinessRules, IReservationRoomWriteRepository reservationRoomWriteRepository)
        {
            _mapper = mapper;
            _reservationRoomReadRepository = reservationRoomReadRepository;
            _reservationRoomBusinessRules = reservationRoomBusinessRules;
            _reservationRoomWriteRepository = reservationRoomWriteRepository;
        }

        public async Task<DeletedReservationRoomResponse> Handle(DeleteReservationRoomCommand request, CancellationToken cancellationToken)
        {
            X.ReservationRoom? reservationRoom = await _reservationRoomReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _reservationRoomBusinessRules.ReservationRoomShouldExistWhenSelected(reservationRoom);
            reservationRoom.DataState = Core.Enum.DataState.Deleted;

            _reservationRoomWriteRepository.Update(reservationRoom);
            await _reservationRoomWriteRepository.SaveAsync();

            return new()
            {
                Title = ReservationRoomsBusinessMessages.ProcessCompleted,
                Message = ReservationRoomsBusinessMessages.SuccessDeletedReservationRoomMessage,
                IsValid = true
            };
        }
    }
}