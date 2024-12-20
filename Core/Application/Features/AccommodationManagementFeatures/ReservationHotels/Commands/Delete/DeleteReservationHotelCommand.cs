using Application.Features.AccommodationManagementFeatures.ReservationHotels.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Delete;

public class DeleteReservationHotelCommand : IRequest<DeletedReservationHotelResponse>
{
	public Guid Gid { get; set; }

    public class DeleteReservationHotelCommandHandler : IRequestHandler<DeleteReservationHotelCommand, DeletedReservationHotelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
        private readonly IReservationHotelWriteRepository _reservationHotelWriteRepository;
        private readonly ReservationHotelBusinessRules _reservationHotelBusinessRules;

        public DeleteReservationHotelCommandHandler(IMapper mapper, IReservationHotelReadRepository reservationHotelReadRepository,
                                         ReservationHotelBusinessRules reservationHotelBusinessRules, IReservationHotelWriteRepository reservationHotelWriteRepository)
        {
            _mapper = mapper;
            _reservationHotelReadRepository = reservationHotelReadRepository;
            _reservationHotelBusinessRules = reservationHotelBusinessRules;
            _reservationHotelWriteRepository = reservationHotelWriteRepository;
        }

        public async Task<DeletedReservationHotelResponse> Handle(DeleteReservationHotelCommand request, CancellationToken cancellationToken)
        {
            X.ReservationHotel? reservationHotel = await _reservationHotelReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _reservationHotelBusinessRules.ReservationHotelShouldExistWhenSelected(reservationHotel);
            reservationHotel.DataState = Core.Enum.DataState.Deleted;

            _reservationHotelWriteRepository.Update(reservationHotel);
            await _reservationHotelWriteRepository.SaveAsync();

            return new()
            {
                Title = ReservationHotelsBusinessMessages.ProcessCompleted,
                Message = ReservationHotelsBusinessMessages.SuccessDeletedReservationHotelMessage,
                IsValid = true
            };
        }
    }
}