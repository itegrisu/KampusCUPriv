using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Rules;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid
{
    public class GetByGidReservationHotelQuery : IRequest<GetByGidReservationHotelResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidReservationHotelQueryHandler : IRequestHandler<GetByGidReservationHotelQuery, GetByGidReservationHotelResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
            private readonly ReservationHotelBusinessRules _reservationHotelBusinessRules;

            public GetByGidReservationHotelQueryHandler(IMapper mapper, IReservationHotelReadRepository reservationHotelReadRepository, ReservationHotelBusinessRules reservationHotelBusinessRules)
            {
                _mapper = mapper;
                _reservationHotelReadRepository = reservationHotelReadRepository;
                _reservationHotelBusinessRules = reservationHotelBusinessRules;
            }

            public async Task<GetByGidReservationHotelResponse> Handle(GetByGidReservationHotelQuery request, CancellationToken cancellationToken)
            {
                X.ReservationHotel? reservationHotel = await _reservationHotelReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(X => X.BuyCurrencyFK).Include(x => x.SellCurrencyFK).Include(x => x.ReservationFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _reservationHotelBusinessRules.ReservationHotelShouldExistWhenSelected(reservationHotel);

                GetByGidReservationHotelResponse response = _mapper.Map<GetByGidReservationHotelResponse>(reservationHotel);
                return response;
            }
        }
    }
}