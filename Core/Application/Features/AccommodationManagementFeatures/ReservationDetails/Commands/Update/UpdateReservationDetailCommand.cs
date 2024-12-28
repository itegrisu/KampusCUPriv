using Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Update;

public class UpdateReservationDetailCommand : IRequest<UpdatedReservationDetailResponse>
{
    public Guid Gid { get; set; }
    public Guid GidReservationHotelFK { get; set; }
    public Guid GidRoomTypeFK { get; set; }
    public DateTime ReservationDate { get; set; }
    public int RoomCount { get; set; }
    public decimal? BuyPrice { get; set; }
    public decimal? SellPrice { get; set; }



    public class UpdateReservationDetailCommandHandler : IRequestHandler<UpdateReservationDetailCommand, UpdatedReservationDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDetailWriteRepository _reservationDetailWriteRepository;
        private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
        private readonly ReservationDetailBusinessRules _reservationDetailBusinessRules;

        public UpdateReservationDetailCommandHandler(IMapper mapper, IReservationDetailWriteRepository reservationDetailWriteRepository,
                                         ReservationDetailBusinessRules reservationDetailBusinessRules, IReservationDetailReadRepository reservationDetailReadRepository)
        {
            _mapper = mapper;
            _reservationDetailWriteRepository = reservationDetailWriteRepository;
            _reservationDetailBusinessRules = reservationDetailBusinessRules;
            _reservationDetailReadRepository = reservationDetailReadRepository;
        }

        public async Task<UpdatedReservationDetailResponse> Handle(UpdateReservationDetailCommand request, CancellationToken cancellationToken)
        {
            await _reservationDetailBusinessRules.IsThereSelectedHotel(request.GidReservationHotelFK);
            await _reservationDetailBusinessRules.IsThereSelectedRoomType(request.GidRoomTypeFK);

            await _reservationDetailBusinessRules.RoomCountCanBeGreaterZero(request.RoomCount);
            await _reservationDetailBusinessRules.ReservationDateControl(request.GidReservationHotelFK, request.ReservationDate);

            X.ReservationDetail? reservationDetail = await _reservationDetailReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK));
            await _reservationDetailBusinessRules.ReservationDetailShouldExistWhenSelected(reservationDetail);
            //INCLUDES Buraya Gelecek include varsa eklenecek

            if (request.ReservationDate != reservationDetail.ReservationDate)
                await _reservationDetailBusinessRules.ReservationDateCanBeUniq(request.GidReservationHotelFK, request.GidRoomTypeFK, request.ReservationDate);

            reservationDetail = _mapper.Map(request, reservationDetail);

            _reservationDetailWriteRepository.Update(reservationDetail!);
            await _reservationDetailWriteRepository.SaveAsync();

            X.ReservationDetail savedReservationDetail = await _reservationDetailReadRepository.GetAsync(predicate: x => x.Gid == reservationDetail.Gid,
              include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK)
               .Include(x => x.ReservationHotelFK).ThenInclude(x => x.BuyCurrencyFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.SellCurrencyFK)
               .Include(x => x.ReservationHotelFK).ThenInclude(x => x.SCCompanyFK));

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