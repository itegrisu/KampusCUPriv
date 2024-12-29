using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Delete;

public class DeleteGuestAccommodationPersonCommand : IRequest<DeletedGuestAccommodationPersonResponse>
{
	public Guid Gid { get; set; }

    public class DeleteGuestAccommodationPersonCommandHandler : IRequestHandler<DeleteGuestAccommodationPersonCommand, DeletedGuestAccommodationPersonResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationPersonReadRepository _guestAccommodationPersonReadRepository;
        private readonly IGuestAccommodationPersonWriteRepository _guestAccommodationPersonWriteRepository;
        private readonly GuestAccommodationPersonBusinessRules _guestAccommodationPersonBusinessRules;

        public DeleteGuestAccommodationPersonCommandHandler(IMapper mapper, IGuestAccommodationPersonReadRepository guestAccommodationPersonReadRepository,
                                         GuestAccommodationPersonBusinessRules guestAccommodationPersonBusinessRules, IGuestAccommodationPersonWriteRepository guestAccommodationPersonWriteRepository)
        {
            _mapper = mapper;
            _guestAccommodationPersonReadRepository = guestAccommodationPersonReadRepository;
            _guestAccommodationPersonBusinessRules = guestAccommodationPersonBusinessRules;
            _guestAccommodationPersonWriteRepository = guestAccommodationPersonWriteRepository;
        }

        public async Task<DeletedGuestAccommodationPersonResponse> Handle(DeleteGuestAccommodationPersonCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodationPerson? guestAccommodationPerson = await _guestAccommodationPersonReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _guestAccommodationPersonBusinessRules.GuestAccommodationPersonShouldExistWhenSelected(guestAccommodationPerson);
            guestAccommodationPerson.DataState = Core.Enum.DataState.Deleted;

            _guestAccommodationPersonWriteRepository.Update(guestAccommodationPerson);
            await _guestAccommodationPersonWriteRepository.SaveAsync();

            return new()
            {
                Title = GuestAccommodationPersonsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationPersonsBusinessMessages.SuccessDeletedGuestAccommodationPersonMessage,
                IsValid = true
            };
        }
    }
}