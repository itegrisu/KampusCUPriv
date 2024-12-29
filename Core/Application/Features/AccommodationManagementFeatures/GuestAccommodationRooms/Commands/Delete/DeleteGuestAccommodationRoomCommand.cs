using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Delete;

public class DeleteGuestAccommodationRoomCommand : IRequest<DeletedGuestAccommodationRoomResponse>
{
	public Guid Gid { get; set; }

    public class DeleteGuestAccommodationRoomCommandHandler : IRequestHandler<DeleteGuestAccommodationRoomCommand, DeletedGuestAccommodationRoomResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;
        private readonly IGuestAccommodationRoomWriteRepository _guestAccommodationRoomWriteRepository;
        private readonly GuestAccommodationRoomBusinessRules _guestAccommodationRoomBusinessRules;

        public DeleteGuestAccommodationRoomCommandHandler(IMapper mapper, IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository,
                                         GuestAccommodationRoomBusinessRules guestAccommodationRoomBusinessRules, IGuestAccommodationRoomWriteRepository guestAccommodationRoomWriteRepository)
        {
            _mapper = mapper;
            _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
            _guestAccommodationRoomBusinessRules = guestAccommodationRoomBusinessRules;
            _guestAccommodationRoomWriteRepository = guestAccommodationRoomWriteRepository;
        }

        public async Task<DeletedGuestAccommodationRoomResponse> Handle(DeleteGuestAccommodationRoomCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodationRoom? guestAccommodationRoom = await _guestAccommodationRoomReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _guestAccommodationRoomBusinessRules.GuestAccommodationRoomShouldExistWhenSelected(guestAccommodationRoom);
            guestAccommodationRoom.DataState = Core.Enum.DataState.Deleted;

            _guestAccommodationRoomWriteRepository.Update(guestAccommodationRoom);
            await _guestAccommodationRoomWriteRepository.SaveAsync();

            return new()
            {
                Title = GuestAccommodationRoomsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationRoomsBusinessMessages.SuccessDeletedGuestAccommodationRoomMessage,
                IsValid = true
            };
        }
    }
}