using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Create;

public class CreateGuestAccommodationRoomCommand : IRequest<CreatedGuestAccommodationRoomResponse>
{
    public Guid GidGuestAccommodationFK { get; set; }
    public Guid GidRoomTypeFK { get; set; }
    public DateTime Date { get; set; }

    public class CreateGuestAccommodationRoomCommandHandler : IRequestHandler<CreateGuestAccommodationRoomCommand, CreatedGuestAccommodationRoomResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationRoomWriteRepository _guestAccommodationRoomWriteRepository;
        private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;
        private readonly GuestAccommodationRoomBusinessRules _guestAccommodationRoomBusinessRules;

        public CreateGuestAccommodationRoomCommandHandler(IMapper mapper, IGuestAccommodationRoomWriteRepository guestAccommodationRoomWriteRepository,
                                         GuestAccommodationRoomBusinessRules guestAccommodationRoomBusinessRules, IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationRoomWriteRepository = guestAccommodationRoomWriteRepository;
            _guestAccommodationRoomBusinessRules = guestAccommodationRoomBusinessRules;
            _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
        }

        public async Task<CreatedGuestAccommodationRoomResponse> Handle(CreateGuestAccommodationRoomCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _guestAccommodationRoomReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.GuestAccommodationRoom guestAccommodationRoom = _mapper.Map<X.GuestAccommodationRoom>(request);
            //guestAccommodationRoom.RowNo = maxRowNo + 1;

            await _guestAccommodationRoomWriteRepository.AddAsync(guestAccommodationRoom);
            await _guestAccommodationRoomWriteRepository.SaveAsync();

            X.GuestAccommodationRoom savedGuestAccommodationRoom = await _guestAccommodationRoomReadRepository.GetAsync(predicate: x => x.Gid == guestAccommodationRoom.Gid, include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidGuestAccommodationRoomResponse obj = _mapper.Map<GetByGidGuestAccommodationRoomResponse>(savedGuestAccommodationRoom);
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