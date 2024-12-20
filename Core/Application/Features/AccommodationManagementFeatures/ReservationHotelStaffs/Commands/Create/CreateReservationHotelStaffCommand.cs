using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Create;

public class CreateReservationHotelStaffCommand : IRequest<CreatedReservationHotelStaffResponse>
{
    public Guid GidHotelFK { get; set; }
    public string FullName { get; set; }
    public string? GsmNo { get; set; }
    public EnumHotelStaffStatus HotelStaffStatus { get; set; }
    public string? Password { get; set; }
    public string? PasswordHash { get; set; }

    public class CreateReservationHotelStaffCommandHandler : IRequestHandler<CreateReservationHotelStaffCommand, CreatedReservationHotelStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelStaffWriteRepository _reservationHotelStaffWriteRepository;
        private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;
        private readonly ReservationHotelStaffBusinessRules _reservationHotelStaffBusinessRules;

        public CreateReservationHotelStaffCommandHandler(IMapper mapper, IReservationHotelStaffWriteRepository reservationHotelStaffWriteRepository,
                                         ReservationHotelStaffBusinessRules reservationHotelStaffBusinessRules, IReservationHotelStaffReadRepository reservationHotelStaffReadRepository)
        {
            _mapper = mapper;
            _reservationHotelStaffWriteRepository = reservationHotelStaffWriteRepository;
            _reservationHotelStaffBusinessRules = reservationHotelStaffBusinessRules;
            _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
        }

        public async Task<CreatedReservationHotelStaffResponse> Handle(CreateReservationHotelStaffCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _reservationHotelStaffReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.ReservationHotelStaff reservationHotelStaff = _mapper.Map<X.ReservationHotelStaff>(request);
            //reservationHotelStaff.RowNo = maxRowNo + 1;

            await _reservationHotelStaffWriteRepository.AddAsync(reservationHotelStaff);
            await _reservationHotelStaffWriteRepository.SaveAsync();

            X.ReservationHotelStaff savedReservationHotelStaff = await _reservationHotelStaffReadRepository.GetAsync(predicate: x => x.Gid == reservationHotelStaff.Gid, include: x => x.Include(x => x.SCCompanyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidReservationHotelStaffResponse obj = _mapper.Map<GetByGidReservationHotelStaffResponse>(savedReservationHotelStaff);
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