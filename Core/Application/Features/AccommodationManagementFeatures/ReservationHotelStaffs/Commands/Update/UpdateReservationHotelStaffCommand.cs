using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Domain.Enums;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Update;

public class UpdateReservationHotelStaffCommand : IRequest<UpdatedReservationHotelStaffResponse>
{
    public Guid Gid { get; set; }

    public Guid GidHotelFK { get; set; }

    public string FullName { get; set; }
    public string? GsmNo { get; set; }
    public EnumHotelStaffStatus HotelStaffStatus { get; set; }
    public string? Password { get; set; }
    public string? PasswordHash { get; set; }



    public class UpdateReservationHotelStaffCommandHandler : IRequestHandler<UpdateReservationHotelStaffCommand, UpdatedReservationHotelStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelStaffWriteRepository _reservationHotelStaffWriteRepository;
        private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;
        private readonly ReservationHotelStaffBusinessRules _reservationHotelStaffBusinessRules;

        public UpdateReservationHotelStaffCommandHandler(IMapper mapper, IReservationHotelStaffWriteRepository reservationHotelStaffWriteRepository,
                                         ReservationHotelStaffBusinessRules reservationHotelStaffBusinessRules, IReservationHotelStaffReadRepository reservationHotelStaffReadRepository)
        {
            _mapper = mapper;
            _reservationHotelStaffWriteRepository = reservationHotelStaffWriteRepository;
            _reservationHotelStaffBusinessRules = reservationHotelStaffBusinessRules;
            _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
        }

        public async Task<UpdatedReservationHotelStaffResponse> Handle(UpdateReservationHotelStaffCommand request, CancellationToken cancellationToken)
        {
            X.ReservationHotelStaff? reservationHotelStaff = await _reservationHotelStaffReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _reservationHotelStaffBusinessRules.ReservationHotelStaffShouldExistWhenSelected(reservationHotelStaff);
            reservationHotelStaff = _mapper.Map(request, reservationHotelStaff);

            _reservationHotelStaffWriteRepository.Update(reservationHotelStaff!);
            await _reservationHotelStaffWriteRepository.SaveAsync();
            GetByGidReservationHotelStaffResponse obj = _mapper.Map<GetByGidReservationHotelStaffResponse>(reservationHotelStaff);

            return new()
            {
                Title = ReservationHotelStaffsBusinessMessages.ProcessCompleted,
                Message = ReservationHotelStaffsBusinessMessages.SuccessCreatedReservationHotelStaffMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}