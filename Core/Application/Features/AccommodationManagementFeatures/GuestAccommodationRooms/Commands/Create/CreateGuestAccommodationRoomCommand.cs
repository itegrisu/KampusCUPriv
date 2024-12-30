using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetList;
using Core.Application.Responses;
using Core.Persistence.Paging;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Create;

public class CreateGuestAccommodationRoomCommand : IRequest<CreatedGuestAccommodationRoomResponse>
{
    public Guid GidGuestAccommodationFK { get; set; }
    public Guid GidRoomTypeFK { get; set; }
    public DateTime Date { get; set; }
    public int Count { get; set; }

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
            var createdRooms = new List<GetByGidGuestAccommodationRoomResponse>();

            for (int i = 0; i < request.Count; i++)
            {
                // Yeni bir entity oluþturuluyor
                X.GuestAccommodationRoom guestAccommodationRoom = _mapper.Map<X.GuestAccommodationRoom>(request);

                // Veritabanýna ekleniyor
                await _guestAccommodationRoomWriteRepository.AddAsync(guestAccommodationRoom);
                await _guestAccommodationRoomWriteRepository.SaveAsync();

                // Eklenen kayýt tekrar okunuyor
                X.GuestAccommodationRoom savedGuestAccommodationRoom = await _guestAccommodationRoomReadRepository.GetAsync(
                    predicate: x => x.Gid == guestAccommodationRoom.Gid,
                    include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK));

                // Response için DTO'ya dönüþtürülüp listeye ekleniyor
                GetByGidGuestAccommodationRoomResponse obj = _mapper.Map<GetByGidGuestAccommodationRoomResponse>(savedGuestAccommodationRoom);
                createdRooms.Add(obj);
            }

            // Dönüþ için yeni bir response oluþturuluyor
            return new CreatedGuestAccommodationRoomResponse
            {
                Title = GuestAccommodationRoomsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationRoomsBusinessMessages.SuccessCreatedGuestAccommodationRoomMessage,
                IsValid = true,
                Obj = createdRooms // Listeyi burada dönüyoruz
            };
        }


    }
}