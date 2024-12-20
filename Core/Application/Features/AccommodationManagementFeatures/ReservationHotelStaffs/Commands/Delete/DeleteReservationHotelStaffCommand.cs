using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Delete;

public class DeleteReservationHotelStaffCommand : IRequest<DeletedReservationHotelStaffResponse>
{
	public Guid Gid { get; set; }

    public class DeleteReservationHotelStaffCommandHandler : IRequestHandler<DeleteReservationHotelStaffCommand, DeletedReservationHotelStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;
        private readonly IReservationHotelStaffWriteRepository _reservationHotelStaffWriteRepository;
        private readonly ReservationHotelStaffBusinessRules _reservationHotelStaffBusinessRules;

        public DeleteReservationHotelStaffCommandHandler(IMapper mapper, IReservationHotelStaffReadRepository reservationHotelStaffReadRepository,
                                         ReservationHotelStaffBusinessRules reservationHotelStaffBusinessRules, IReservationHotelStaffWriteRepository reservationHotelStaffWriteRepository)
        {
            _mapper = mapper;
            _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
            _reservationHotelStaffBusinessRules = reservationHotelStaffBusinessRules;
            _reservationHotelStaffWriteRepository = reservationHotelStaffWriteRepository;
        }

        public async Task<DeletedReservationHotelStaffResponse> Handle(DeleteReservationHotelStaffCommand request, CancellationToken cancellationToken)
        {
            X.ReservationHotelStaff? reservationHotelStaff = await _reservationHotelStaffReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _reservationHotelStaffBusinessRules.ReservationHotelStaffShouldExistWhenSelected(reservationHotelStaff);
            reservationHotelStaff.DataState = Core.Enum.DataState.Deleted;

            _reservationHotelStaffWriteRepository.Update(reservationHotelStaff);
            await _reservationHotelStaffWriteRepository.SaveAsync();

            return new()
            {
                Title = ReservationHotelStaffsBusinessMessages.ProcessCompleted,
                Message = ReservationHotelStaffsBusinessMessages.SuccessDeletedReservationHotelStaffMessage,
                IsValid = true
            };
        }
    }
}