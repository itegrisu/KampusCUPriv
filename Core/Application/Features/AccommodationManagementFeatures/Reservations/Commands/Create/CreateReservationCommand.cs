using Application.Features.AccommodationManagementFeatures.Reservations.Constants;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Reservations.Rules;
using Application.Repositories.AccommodationManagements.ReservationRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Commands.Create;

public class CreateReservationCommand : IRequest<CreatedReservationResponse>
{
    public Guid? GidOrganizationFK { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? EstimatedGuestCount { get; set; }
    public int? EstimatedAccommodationCount { get; set; }
    public EnumReservationType ReservationType { get; set; }
    public EnumReservationStatus ReservationStatus { get; set; }

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, CreatedReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;

        public CreateReservationCommandHandler(IMapper mapper, IReservationWriteRepository reservationWriteRepository,
                                         ReservationBusinessRules reservationBusinessRules, IReservationReadRepository reservationReadRepository)
        {
            _mapper = mapper;
            _reservationWriteRepository = reservationWriteRepository;
            _reservationBusinessRules = reservationBusinessRules;
            _reservationReadRepository = reservationReadRepository;
        }

        public async Task<CreatedReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _reservationReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Reservation reservation = _mapper.Map<X.Reservation>(request);
            //reservation.RowNo = maxRowNo + 1;

            await _reservationWriteRepository.AddAsync(reservation);
            await _reservationWriteRepository.SaveAsync();

            X.Reservation savedReservation = await _reservationReadRepository.GetAsync(predicate: x => x.Gid == reservation.Gid, include: x => x.Include(x => x.OrganizationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidReservationResponse obj = _mapper.Map<GetByGidReservationResponse>(savedReservation);
            return new()
            {
                Title = ReservationsBusinessMessages.ProcessCompleted,
                Message = ReservationsBusinessMessages.SuccessCreatedReservationMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}