using Application.Features.AccommodationManagementFeatures.Reservations.Constants;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Reservations.Rules;
using Application.Repositories.AccommodationManagements.ReservationRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Commands.Update;

public class UpdateReservationCommand : IRequest<UpdatedReservationResponse>
{
    public Guid Gid { get; set; }
    public Guid? GidOrganizationFK { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? EstimatedGuestCount { get; set; }
    public int? EstimatedAccommodationCount { get; set; }
    public EnumReservationType ReservationType { get; set; }
    public EnumReservationStatus ReservationStatus { get; set; }

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, UpdatedReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;

        public UpdateReservationCommandHandler(IMapper mapper, IReservationWriteRepository reservationWriteRepository,
                                         ReservationBusinessRules reservationBusinessRules, IReservationReadRepository reservationReadRepository)
        {
            _mapper = mapper;
            _reservationWriteRepository = reservationWriteRepository;
            _reservationBusinessRules = reservationBusinessRules;
            _reservationReadRepository = reservationReadRepository;
        }

        public async Task<UpdatedReservationResponse> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            X.Reservation? reservation = await _reservationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OrganizationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _reservationBusinessRules.ReservationShouldExistWhenSelected(reservation);
            reservation = _mapper.Map(request, reservation);

            _reservationWriteRepository.Update(reservation!);
            await _reservationWriteRepository.SaveAsync();

            X.Reservation? newReservation = await _reservationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OrganizationFK));

            GetByGidReservationResponse obj = _mapper.Map<GetByGidReservationResponse>(newReservation);

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