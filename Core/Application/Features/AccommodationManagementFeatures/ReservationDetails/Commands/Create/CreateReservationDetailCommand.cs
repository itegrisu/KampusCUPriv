using Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Create;

public class CreateReservationDetailCommand : IRequest<CreatedReservationDetailResponse>
{
    public Guid GidReservationHotelFK { get; set; }
    public Guid GidRoomTypeFK { get; set; }
    public DateTime ReservationDate { get; set; }
    public int RoomCount { get; set; }
    public decimal? BuyPrice { get; set; }
    public decimal? SellPrice { get; set; }

    public class CreateReservationDetailCommandHandler : IRequestHandler<CreateReservationDetailCommand, CreatedReservationDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDetailWriteRepository _reservationDetailWriteRepository;
        private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
        private readonly ReservationDetailBusinessRules _reservationDetailBusinessRules;

        public CreateReservationDetailCommandHandler(IMapper mapper, IReservationDetailWriteRepository reservationDetailWriteRepository,
                                         ReservationDetailBusinessRules reservationDetailBusinessRules, IReservationDetailReadRepository reservationDetailReadRepository)
        {
            _mapper = mapper;
            _reservationDetailWriteRepository = reservationDetailWriteRepository;
            _reservationDetailBusinessRules = reservationDetailBusinessRules;
            _reservationDetailReadRepository = reservationDetailReadRepository;
        }

        public async Task<CreatedReservationDetailResponse> Handle(CreateReservationDetailCommand request, CancellationToken cancellationToken)
        {
            await _reservationDetailBusinessRules.IsThereSelectedHotel(request.GidReservationHotelFK);
            await _reservationDetailBusinessRules.IsThereSelectedRoomType(request.GidRoomTypeFK);

            await _reservationDetailBusinessRules.RoomCountCanBeGreaterZero(request.RoomCount);
            await _reservationDetailBusinessRules.ReservationDateControl(request.GidReservationHotelFK, request.ReservationDate);
            await _reservationDetailBusinessRules.ReservationDateCanBeUniq(request.GidReservationHotelFK, request.GidRoomTypeFK, request.ReservationDate);


            //int maxRowNo = await _reservationDetailReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.ReservationDetail reservationDetail = _mapper.Map<X.ReservationDetail>(request);
            //reservationDetail.RowNo = maxRowNo + 1;

            await _reservationDetailWriteRepository.AddAsync(reservationDetail);
            await _reservationDetailWriteRepository.SaveAsync();

            X.ReservationDetail savedReservationDetail = await _reservationDetailReadRepository.GetAsync(predicate: x => x.Gid == reservationDetail.Gid,
                include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK)
                 .Include(x => x.ReservationHotelFK).ThenInclude(x => x.BuyCurrencyFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.SellCurrencyFK)
                 .Include(x => x.ReservationHotelFK).ThenInclude(x => x.SCCompanyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidReservationDetailResponse obj = _mapper.Map<GetByGidReservationDetailResponse>(savedReservationDetail);
            return new()
            {
                Title = ReservationDetailsBusinessMessages.ProcessCompleted,
                Message = ReservationDetailsBusinessMessages.SuccessCreatedReservationDetailMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}