using Application.Features.AccommodationManagementFeatures.Guests.Constants;
using Application.Features.AccommodationManagementFeatures.Guests.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestRepo;

namespace Application.Features.AccommodationManagementFeatures.Guests.Commands.Delete;

public class DeleteGuestCommand : IRequest<DeletedGuestResponse>
{
	public Guid Gid { get; set; }

    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand, DeletedGuestResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestReadRepository _guestReadRepository;
        private readonly IGuestWriteRepository _guestWriteRepository;
        private readonly GuestBusinessRules _guestBusinessRules;

        public DeleteGuestCommandHandler(IMapper mapper, IGuestReadRepository guestReadRepository,
                                         GuestBusinessRules guestBusinessRules, IGuestWriteRepository guestWriteRepository)
        {
            _mapper = mapper;
            _guestReadRepository = guestReadRepository;
            _guestBusinessRules = guestBusinessRules;
            _guestWriteRepository = guestWriteRepository;
        }

        public async Task<DeletedGuestResponse> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            X.Guest? guest = await _guestReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _guestBusinessRules.GuestShouldExistWhenSelected(guest);
            guest.DataState = Core.Enum.DataState.Deleted;

            _guestWriteRepository.Update(guest);
            await _guestWriteRepository.SaveAsync();

            return new()
            {
                Title = GuestsBusinessMessages.ProcessCompleted,
                Message = GuestsBusinessMessages.SuccessDeletedGuestMessage,
                IsValid = true
            };
        }
    }
}