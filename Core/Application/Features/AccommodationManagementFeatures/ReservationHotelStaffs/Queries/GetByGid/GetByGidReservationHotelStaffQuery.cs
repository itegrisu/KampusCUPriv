using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Rules;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid
{
    public class GetByGidReservationHotelStaffQuery : IRequest<GetByGidReservationHotelStaffResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidReservationHotelStaffQueryHandler : IRequestHandler<GetByGidReservationHotelStaffQuery, GetByGidReservationHotelStaffResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;
            private readonly ReservationHotelStaffBusinessRules _reservationHotelStaffBusinessRules;

            public GetByGidReservationHotelStaffQueryHandler(IMapper mapper, IReservationHotelStaffReadRepository reservationHotelStaffReadRepository, ReservationHotelStaffBusinessRules reservationHotelStaffBusinessRules)
            {
                _mapper = mapper;
                _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
                _reservationHotelStaffBusinessRules = reservationHotelStaffBusinessRules;
            }

            public async Task<GetByGidReservationHotelStaffResponse> Handle(GetByGidReservationHotelStaffQuery request, CancellationToken cancellationToken)
            {
                X.ReservationHotelStaff? reservationHotelStaff = await _reservationHotelStaffReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _reservationHotelStaffBusinessRules.ReservationHotelStaffShouldExistWhenSelected(reservationHotelStaff);

                GetByGidReservationHotelStaffResponse response = _mapper.Map<GetByGidReservationHotelStaffResponse>(reservationHotelStaff);
                return response;
            }
        }
    }
}