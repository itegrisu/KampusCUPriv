using Application.Features.AccommodationManagementFeatures.ReservationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Rules;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

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
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;

        public DeleteReservationRoomCommandHandler(IMapper mapper, IReservationRoomReadRepository reservationRoomReadRepository,
                                         ReservationRoomBusinessRules reservationRoomBusinessRules, IReservationRoomWriteRepository reservationRoomWriteRepository, IAccommodationDateReadRepository accommodationDateReadRepository)
        {
            _mapper = mapper;
            _reservationRoomReadRepository = reservationRoomReadRepository;
            _reservationRoomBusinessRules = reservationRoomBusinessRules;
            _reservationRoomWriteRepository = reservationRoomWriteRepository;
            _accommodationDateReadRepository = accommodationDateReadRepository;
        }

        public async Task<DeletedReservationRoomResponse> Handle(DeleteReservationRoomCommand request, CancellationToken cancellationToken)
        {
            X.ReservationRoom? reservationRoom = await _reservationRoomReadRepository.GetAsync(predicate: x => x.Gid == request.Gid,
                include: x => x.Include(x => x.AccommodationDates), cancellationToken: cancellationToken);
            await _reservationRoomBusinessRules.ReservationRoomShouldExistWhenSelected(reservationRoom);
            await _reservationRoomBusinessRules.ReservationRoomShouldNotHaveAccommodationDates(reservationRoom);

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