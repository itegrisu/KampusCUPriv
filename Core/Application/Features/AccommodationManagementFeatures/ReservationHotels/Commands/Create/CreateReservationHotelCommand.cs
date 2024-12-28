using Application.Features.AccommodationManagementFeatures.ReservationHotels.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Rules;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Create;

public class CreateReservationHotelCommand : IRequest<CreatedReservationHotelResponse>
{
    public Guid GidReservationFK { get; set; }
    public Guid GidHotelFK { get; set; }
    public Guid GidBuyCurrencyTypeFK { get; set; }
    public Guid GidSellCurrencyTypeFK { get; set; }


    public class CreateReservationHotelCommandHandler : IRequestHandler<CreateReservationHotelCommand, CreatedReservationHotelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelWriteRepository _reservationHotelWriteRepository;
        private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
        private readonly ReservationHotelBusinessRules _reservationHotelBusinessRules;

        public CreateReservationHotelCommandHandler(IMapper mapper, IReservationHotelWriteRepository reservationHotelWriteRepository,
                                         ReservationHotelBusinessRules reservationHotelBusinessRules, IReservationHotelReadRepository reservationHotelReadRepository)
        {
            _mapper = mapper;
            _reservationHotelWriteRepository = reservationHotelWriteRepository;
            _reservationHotelBusinessRules = reservationHotelBusinessRules;
            _reservationHotelReadRepository = reservationHotelReadRepository;
        }

        public async Task<CreatedReservationHotelResponse> Handle(CreateReservationHotelCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _reservationHotelReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.ReservationHotel reservationHotel = _mapper.Map<X.ReservationHotel>(request);
            //reservationHotel.RowNo = maxRowNo + 1;

            await _reservationHotelBusinessRules.ReservationHotelAlreadyAdded(request.GidReservationFK, request.GidHotelFK);

            await _reservationHotelWriteRepository.AddAsync(reservationHotel);
            await _reservationHotelWriteRepository.SaveAsync();

            X.ReservationHotel savedReservationHotel = await _reservationHotelReadRepository.GetAsync(predicate: x => x.Gid == reservationHotel.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(X => X.BuyCurrencyFK).Include(x => x.SellCurrencyFK).Include(x => x.ReservationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidReservationHotelResponse obj = _mapper.Map<GetByGidReservationHotelResponse>(savedReservationHotel);
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