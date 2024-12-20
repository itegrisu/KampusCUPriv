using Application.Features.AccommodationManagementFeatures.ReservationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Update;

public class UpdateReservationRoomCommand : IRequest<UpdatedReservationRoomResponse>
{
    public Guid Gid { get; set; }

	public Guid GidReservationDetailFK { get; set; }

public int RoomNo { get; set; }



    public class UpdateReservationRoomCommandHandler : IRequestHandler<UpdateReservationRoomCommand, UpdatedReservationRoomResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRoomWriteRepository _reservationRoomWriteRepository;
        private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
        private readonly ReservationRoomBusinessRules _reservationRoomBusinessRules;

        public UpdateReservationRoomCommandHandler(IMapper mapper, IReservationRoomWriteRepository reservationRoomWriteRepository,
                                         ReservationRoomBusinessRules reservationRoomBusinessRules, IReservationRoomReadRepository reservationRoomReadRepository)
        {
            _mapper = mapper;
            _reservationRoomWriteRepository = reservationRoomWriteRepository;
            _reservationRoomBusinessRules = reservationRoomBusinessRules;
            _reservationRoomReadRepository = reservationRoomReadRepository;
        }

        public async Task<UpdatedReservationRoomResponse> Handle(UpdateReservationRoomCommand request, CancellationToken cancellationToken)
        {
            X.ReservationRoom? reservationRoom = await _reservationRoomReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ReservationDetailFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _reservationRoomBusinessRules.ReservationRoomShouldExistWhenSelected(reservationRoom);
            reservationRoom = _mapper.Map(request, reservationRoom);

            _reservationRoomWriteRepository.Update(reservationRoom!);
            await _reservationRoomWriteRepository.SaveAsync();
            GetByGidReservationRoomResponse obj = _mapper.Map<GetByGidReservationRoomResponse>(reservationRoom);

            return new()
            {
                Title = ReservationRoomsBusinessMessages.ProcessCompleted,
                Message = ReservationRoomsBusinessMessages.SuccessCreatedReservationRoomMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}