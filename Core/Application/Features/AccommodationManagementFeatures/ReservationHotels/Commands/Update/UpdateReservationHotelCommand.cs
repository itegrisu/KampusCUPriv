using Application.Features.AccommodationManagementFeatures.ReservationHotels.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Update;

public class UpdateReservationHotelCommand : IRequest<UpdatedReservationHotelResponse>
{
    public Guid Gid { get; set; }

	public Guid GidReservationFK { get; set; }
public Guid GidHotelFK { get; set; }
public Guid GidBuyCurrencyTypeFK { get; set; }
public Guid GidSellCurrencyTypeFK { get; set; }




    public class UpdateReservationHotelCommandHandler : IRequestHandler<UpdateReservationHotelCommand, UpdatedReservationHotelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelWriteRepository _reservationHotelWriteRepository;
        private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
        private readonly ReservationHotelBusinessRules _reservationHotelBusinessRules;

        public UpdateReservationHotelCommandHandler(IMapper mapper, IReservationHotelWriteRepository reservationHotelWriteRepository,
                                         ReservationHotelBusinessRules reservationHotelBusinessRules, IReservationHotelReadRepository reservationHotelReadRepository)
        {
            _mapper = mapper;
            _reservationHotelWriteRepository = reservationHotelWriteRepository;
            _reservationHotelBusinessRules = reservationHotelBusinessRules;
            _reservationHotelReadRepository = reservationHotelReadRepository;
        }

        public async Task<UpdatedReservationHotelResponse> Handle(UpdateReservationHotelCommand request, CancellationToken cancellationToken)
        {
            X.ReservationHotel? reservationHotel = await _reservationHotelReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(X => X.BuyCurrencyFK).Include(x => x.SellCurrencyFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _reservationHotelBusinessRules.ReservationHotelShouldExistWhenSelected(reservationHotel);
            reservationHotel = _mapper.Map(request, reservationHotel);

            _reservationHotelWriteRepository.Update(reservationHotel!);
            await _reservationHotelWriteRepository.SaveAsync();
            GetByGidReservationHotelResponse obj = _mapper.Map<GetByGidReservationHotelResponse>(reservationHotel);

            return new()
            {
                Title = ReservationHotelsBusinessMessages.ProcessCompleted,
                Message = ReservationHotelsBusinessMessages.SuccessCreatedReservationHotelMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}