using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Update;

public class UpdateGuestAccommodationRoomCommand : IRequest<UpdatedGuestAccommodationRoomResponse>
{
    public Guid Gid { get; set; }
    public Guid GidGuestAccommodationFK { get; set; }
    public Guid GidRoomTypeFK { get; set; }
    public DateTime Date { get; set; }

    public class UpdateGuestAccommodationRoomCommandHandler : IRequestHandler<UpdateGuestAccommodationRoomCommand, UpdatedGuestAccommodationRoomResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationRoomWriteRepository _guestAccommodationRoomWriteRepository;
        private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;
        private readonly GuestAccommodationRoomBusinessRules _guestAccommodationRoomBusinessRules;

        public UpdateGuestAccommodationRoomCommandHandler(IMapper mapper, IGuestAccommodationRoomWriteRepository guestAccommodationRoomWriteRepository,
                                         GuestAccommodationRoomBusinessRules guestAccommodationRoomBusinessRules, IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationRoomWriteRepository = guestAccommodationRoomWriteRepository;
            _guestAccommodationRoomBusinessRules = guestAccommodationRoomBusinessRules;
            _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
        }

        public async Task<UpdatedGuestAccommodationRoomResponse> Handle(UpdateGuestAccommodationRoomCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodationRoom? guestAccommodationRoom = await _guestAccommodationRoomReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _guestAccommodationRoomBusinessRules.GuestAccommodationRoomShouldExistWhenSelected(guestAccommodationRoom);
            guestAccommodationRoom = _mapper.Map(request, guestAccommodationRoom);

            _guestAccommodationRoomWriteRepository.Update(guestAccommodationRoom!);
            await _guestAccommodationRoomWriteRepository.SaveAsync();

            X.GuestAccommodationRoom? guestAccommodationRoomUpdated = await _guestAccommodationRoomReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK));

            GetByGidGuestAccommodationRoomResponse obj = _mapper.Map<GetByGidGuestAccommodationRoomResponse>(guestAccommodationRoomUpdated);

            return new()
            {
                Title = GuestAccommodationRoomsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationRoomsBusinessMessages.SuccessCreatedGuestAccommodationRoomMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}