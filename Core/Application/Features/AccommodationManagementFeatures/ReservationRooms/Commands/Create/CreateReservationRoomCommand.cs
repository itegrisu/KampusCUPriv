using Application.Features.AccommodationManagementFeatures.ReservationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Rules;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Create;

public class CreateReservationRoomCommand : IRequest<CreatedReservationRoomResponse>
{
    public Guid GidReservationDetailFK { get; set; }
    public int RoomNo { get; set; }

    public class CreateReservationRoomCommandHandler : IRequestHandler<CreateReservationRoomCommand, CreatedReservationRoomResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRoomWriteRepository _reservationRoomWriteRepository;
        private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
        private readonly ReservationRoomBusinessRules _reservationRoomBusinessRules;

        public CreateReservationRoomCommandHandler(IMapper mapper, IReservationRoomWriteRepository reservationRoomWriteRepository,
                                         ReservationRoomBusinessRules reservationRoomBusinessRules, IReservationRoomReadRepository reservationRoomReadRepository)
        {
            _mapper = mapper;
            _reservationRoomWriteRepository = reservationRoomWriteRepository;
            _reservationRoomBusinessRules = reservationRoomBusinessRules;
            _reservationRoomReadRepository = reservationRoomReadRepository;
        }

        public async Task<CreatedReservationRoomResponse> Handle(CreateReservationRoomCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _reservationRoomReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.ReservationRoom reservationRoom = _mapper.Map<X.ReservationRoom>(request);
            //reservationRoom.RowNo = maxRowNo + 1;

            await _reservationRoomWriteRepository.AddAsync(reservationRoom);
            await _reservationRoomWriteRepository.SaveAsync();

            X.ReservationRoom savedReservationRoom = await _reservationRoomReadRepository.GetAsync(predicate: x => x.Gid == reservationRoom.Gid,
                include: x => x.Include(x => x.ReservationDetailFK).Include(x => x.ReservationDetailFK).ThenInclude(x => x.RoomTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidReservationRoomResponse obj = _mapper.Map<GetByGidReservationRoomResponse>(savedReservationRoom);
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