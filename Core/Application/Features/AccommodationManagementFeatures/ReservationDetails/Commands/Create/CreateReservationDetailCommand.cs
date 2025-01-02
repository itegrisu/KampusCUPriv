using Application.Abstractions.UnitOfWork;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Create;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
        private readonly IMediator _mediator;

        public CreateReservationDetailCommandHandler(IMapper mapper, IReservationDetailWriteRepository reservationDetailWriteRepository,
                                         ReservationDetailBusinessRules reservationDetailBusinessRules, IReservationDetailReadRepository reservationDetailReadRepository, IUnitOfWork unitOfWork, IReservationRoomReadRepository reservationRoomReadRepository, IMediator mediator)
        {
            _mapper = mapper;
            _reservationDetailWriteRepository = reservationDetailWriteRepository;
            _reservationDetailBusinessRules = reservationDetailBusinessRules;
            _reservationDetailReadRepository = reservationDetailReadRepository;
            _unitOfWork = unitOfWork;
            _reservationRoomReadRepository = reservationRoomReadRepository;
            _mediator = mediator;
        }

        public async Task<CreatedReservationDetailResponse> Handle(CreateReservationDetailCommand request, CancellationToken cancellationToken)
        {
            await _reservationDetailBusinessRules.IsThereSelectedHotel(request.GidReservationHotelFK);
            await _reservationDetailBusinessRules.IsThereSelectedRoomType(request.GidRoomTypeFK);

            await _reservationDetailBusinessRules.RoomCountCanBeGreaterZero(request.RoomCount);
            await _reservationDetailBusinessRules.ReservationDateControl(request.GidReservationHotelFK, request.ReservationDate);
            await _reservationDetailBusinessRules.ReservationDateCanBeUniq(request.GidReservationHotelFK, request.GidRoomTypeFK, request.ReservationDate);

            _unitOfWork.BeginTransaction();
            X.ReservationDetail reservationDetail;
            try
            {
                reservationDetail = _mapper.Map<X.ReservationDetail>(request);

                await _reservationDetailWriteRepository.AddAsync(reservationDetail);
                await _reservationDetailWriteRepository.SaveAsync();

                for (int i = 0; i < request.RoomCount; i++)
                {
                    CreateReservationRoomCommand command = new()
                    {
                        GidReservationDetailFK = reservationDetail.Gid,
                        RoomNo = i + 1
                    };

                    CreatedReservationRoomResponse response = await _mediator.Send(command);
                }

                // Deðiþiklikler kaydedilir ve transaction tamamlanýr
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new BusinessException("Transaction sýrasýnda bir hata oluþtu.", ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }



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