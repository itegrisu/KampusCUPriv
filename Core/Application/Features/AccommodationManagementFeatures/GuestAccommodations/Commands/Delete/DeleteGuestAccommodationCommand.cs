using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Delete;

public class DeleteGuestAccommodationCommand : IRequest<DeletedGuestAccommodationResponse>
{
	public Guid Gid { get; set; }

    public class DeleteGuestAccommodationCommandHandler : IRequestHandler<DeleteGuestAccommodationCommand, DeletedGuestAccommodationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationReadRepository _guestAccommodationReadRepository;
        private readonly IGuestAccommodationWriteRepository _guestAccommodationWriteRepository;
        private readonly GuestAccommodationBusinessRules _guestAccommodationBusinessRules;

        public DeleteGuestAccommodationCommandHandler(IMapper mapper, IGuestAccommodationReadRepository guestAccommodationReadRepository,
                                         GuestAccommodationBusinessRules guestAccommodationBusinessRules, IGuestAccommodationWriteRepository guestAccommodationWriteRepository)
        {
            _mapper = mapper;
            _guestAccommodationReadRepository = guestAccommodationReadRepository;
            _guestAccommodationBusinessRules = guestAccommodationBusinessRules;
            _guestAccommodationWriteRepository = guestAccommodationWriteRepository;
        }

        public async Task<DeletedGuestAccommodationResponse> Handle(DeleteGuestAccommodationCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodation? guestAccommodation = await _guestAccommodationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _guestAccommodationBusinessRules.GuestAccommodationShouldExistWhenSelected(guestAccommodation);
            guestAccommodation.DataState = Core.Enum.DataState.Deleted;

            _guestAccommodationWriteRepository.Update(guestAccommodation);
            await _guestAccommodationWriteRepository.SaveAsync();

            return new()
            {
                Title = GuestAccommodationsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationsBusinessMessages.SuccessDeletedGuestAccommodationMessage,
                IsValid = true
            };
        }
    }
}