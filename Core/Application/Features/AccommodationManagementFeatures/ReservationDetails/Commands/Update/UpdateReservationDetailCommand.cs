using Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Microsoft.EntityFrameworkCore;

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
            X.ReservationDetail? reservationDetail = await _reservationDetailReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _reservationDetailBusinessRules.ReservationDetailShouldExistWhenSelected(reservationDetail);
            reservationDetail = _mapper.Map(request, reservationDetail);

            _reservationDetailWriteRepository.Update(reservationDetail!);
            await _reservationDetailWriteRepository.SaveAsync();
            GetByGidReservationDetailResponse obj = _mapper.Map<GetByGidReservationDetailResponse>(reservationDetail);

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