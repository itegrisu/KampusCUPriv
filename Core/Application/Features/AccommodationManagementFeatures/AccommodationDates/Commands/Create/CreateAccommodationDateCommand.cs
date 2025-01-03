using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Create;

public class CreateAccommodationDateCommand : IRequest<CreatedAccommodationDateResponse>
{
    public Guid GidReservationDetailFK { get; set; }
    public Guid GidGuestFK { get; set; }
    public Guid GidRoomNoFK { get; set; }

    public Guid GidReservationHotelFK { get; set; }

    public DateTime[] Date { get; set; }
    public string? PreviousRoomInfo { get; set; }



    public class CreateAccommodationDateCommandHandler : IRequestHandler<CreateAccommodationDateCommand, CreatedAccommodationDateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccommodationDateWriteRepository _accommodationDateWriteRepository;
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
        private readonly AccommodationDateBusinessRules _accommodationDateBusinessRules;
        private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
        private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
        private readonly IReservationRoomReadRepository _reservationRoomReadRepository;

        public CreateAccommodationDateCommandHandler(IMapper mapper, IAccommodationDateWriteRepository accommodationDateWriteRepository,
                                         AccommodationDateBusinessRules accommodationDateBusinessRules, IAccommodationDateReadRepository accommodationDateReadRepository, IReservationHotelReadRepository reservationHotelReadRepository, IReservationDetailReadRepository reservationDetailReadRepository, IReservationRoomReadRepository reservationRoomReadRepository)
        {
            _mapper = mapper;
            _accommodationDateWriteRepository = accommodationDateWriteRepository;
            _accommodationDateBusinessRules = accommodationDateBusinessRules;
            _accommodationDateReadRepository = accommodationDateReadRepository;
            _reservationHotelReadRepository = reservationHotelReadRepository;
            _reservationDetailReadRepository = reservationDetailReadRepository;
            _reservationRoomReadRepository = reservationRoomReadRepository;
        }

        public async Task<CreatedAccommodationDateResponse> Handle(CreateAccommodationDateCommand request, CancellationToken cancellationToken)
        {
            await _accommodationDateBusinessRules.IsReservationHotelFKExist(request.GidReservationHotelFK);
            await _accommodationDateBusinessRules.IsReservationDetailFKExist(request.GidReservationDetailFK);
            await _accommodationDateBusinessRules.IsGuestFKExist(request.GidGuestFK);
            await _accommodationDateBusinessRules.IsRoomNoFKExist(request.GidRoomNoFK);

            await _accommodationDateBusinessRules.ValidateRoomCapacity(request.GidReservationDetailFK, request.GidRoomNoFK, request.Date);
            await _accommodationDateBusinessRules.GuestControl(request.GidReservationDetailFK, request.GidGuestFK, request.Date);
            await _accommodationDateBusinessRules.ValidateRoomTypeAvailability(request.GidReservationDetailFK, request.Date, request.GidRoomNoFK);

            var initialRoom = await _reservationRoomReadRepository.GetAsync(
                x => x.Gid == request.GidRoomNoFK,
               include: x => x.Include(r => r.ReservationDetailFK)
                    );

            if (initialRoom == null)
            {
                throw new BusinessException("Ýstenilen oda bulunamadý.");
            }

            var desiredRoomNo = initialRoom.RoomNo; // Ýstenilen oda numarasý
            var roomTypeGid = initialRoom.ReservationDetailFK.GidRoomTypeFK; // Oda tipi

            // Tüm tarihler için ReservationDetail bilgilerini çek
            var reservationDetails = await _reservationDetailReadRepository.GetAll()
                .Where(rd => request.Date.Contains(rd.ReservationDate.Date) && rd.GidRoomTypeFK == roomTypeGid && rd.GidReservationHotelFK == request.GidReservationHotelFK)
                .Include(rd => rd.ReservationRooms) // ReservationRooms bilgisi dahil ediliyor
                .ToListAsync();

            foreach (var date in request.Date)
            {
                // Ýlgili tarih için ReservationDetail al
                var currentReservationDetail = reservationDetails.FirstOrDefault(rd => rd.ReservationDate.Date == date.Date);

                if (currentReservationDetail == null)//Silinebilir
                {
                    throw new BusinessException($"{date:dd.MM.yyyy} tarihinde rezervasyon detayý bulunamadý.");
                }

                // Ýlgili tarih için oda numarasýný eþleþtiren oda seç
                var matchingRoom = currentReservationDetail.ReservationRooms
                    .FirstOrDefault(rr => rr.RoomNo == desiredRoomNo);

                if (matchingRoom == null)//Silinebilir
                {
                    throw new BusinessException($"{date:dd.MM.yyyy} tarihinde {desiredRoomNo} numaralý oda bulunamadý veya uygun deðil.");
                }

                // AccommodationDate oluþtur ve kaydet
                X.AccommodationDate accommodationDate = new()
                {
                    Date = date,
                    GidGuestFK = request.GidGuestFK,
                    GidReservationDetailFK = currentReservationDetail.Gid,
                    GidRoomNoFK = matchingRoom.Gid, // Bulunan oda Gid
                    PreviousRoomInfo = $"Oda numarasý: {desiredRoomNo}, Tarih: {date:dd.MM.yyyy}"
                };

                await _accommodationDateWriteRepository.AddAsync(accommodationDate);
                await _accommodationDateWriteRepository.SaveAsync();
            }

            return new()
            {
                Title = AccommodationDatesBusinessMessages.ProcessCompleted,
                Message = AccommodationDatesBusinessMessages.SuccessCreatedAccommodationDateMessage,
                IsValid = true,
            };
        }
    }
}