using Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Delete;

public class DeleteReservationDetailCommand : IRequest<DeletedReservationDetailResponse>
{
    public Guid Gid { get; set; }

    public class DeleteReservationDetailCommandHandler : IRequestHandler<DeleteReservationDetailCommand, DeletedReservationDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
        private readonly IReservationDetailWriteRepository _reservationDetailWriteRepository;
        private readonly ReservationDetailBusinessRules _reservationDetailBusinessRules;

        public DeleteReservationDetailCommandHandler(IMapper mapper, IReservationDetailReadRepository reservationDetailReadRepository,
                                         ReservationDetailBusinessRules reservationDetailBusinessRules, IReservationDetailWriteRepository reservationDetailWriteRepository)
        {
            _mapper = mapper;
            _reservationDetailReadRepository = reservationDetailReadRepository;
            _reservationDetailBusinessRules = reservationDetailBusinessRules;
            _reservationDetailWriteRepository = reservationDetailWriteRepository;
        }

        public async Task<DeletedReservationDetailResponse> Handle(DeleteReservationDetailCommand request, CancellationToken cancellationToken)
        {
            X.ReservationDetail? reservationDetail = await _reservationDetailReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _reservationDetailBusinessRules.ReservationDetailShouldExistWhenSelected(reservationDetail);
            reservationDetail.DataState = Core.Enum.DataState.Deleted;

            _reservationDetailWriteRepository.Update(reservationDetail);
            await _reservationDetailWriteRepository.SaveAsync();

            return new()
            {
                Title = ReservationDetailsBusinessMessages.ProcessCompleted,
                Message = ReservationDetailsBusinessMessages.SuccessDeletedReservationDetailMessage,
                IsValid = true
            };
        }
    }
}