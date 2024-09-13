using Application.Features.DefinitionManagementFeatures.RoomTypes.Constants;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Delete;

public class DeleteRoomTypeCommand : IRequest<DeletedRoomTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, DeletedRoomTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRoomTypeReadRepository _roomTypeReadRepository;
        private readonly IRoomTypeWriteRepository _roomTypeWriteRepository;
        private readonly RoomTypeBusinessRules _roomTypeBusinessRules;

        public DeleteRoomTypeCommandHandler(IMapper mapper, IRoomTypeReadRepository roomTypeReadRepository,
                                         RoomTypeBusinessRules roomTypeBusinessRules, IRoomTypeWriteRepository roomTypeWriteRepository)
        {
            _mapper = mapper;
            _roomTypeReadRepository = roomTypeReadRepository;
            _roomTypeBusinessRules = roomTypeBusinessRules;
            _roomTypeWriteRepository = roomTypeWriteRepository;
        }

        public async Task<DeletedRoomTypeResponse> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            X.RoomType? roomType = await _roomTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _roomTypeBusinessRules.RoomTypeShouldExistWhenSelected(roomType);
            roomType.DataState = Core.Enum.DataState.Deleted;

            _roomTypeWriteRepository.Update(roomType);
            await _roomTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = RoomTypesBusinessMessages.ProcessCompleted,
                Message = RoomTypesBusinessMessages.SuccessDeletedRoomTypeMessage,
                IsValid = true
            };
        }
    }
}