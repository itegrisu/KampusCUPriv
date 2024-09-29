using Application.Features.DefinitionManagementFeatures.RoomTypes.Constants;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Update;

public class UpdateRoomTypeCommand : IRequest<UpdatedRoomTypeResponse>
{
    public Guid Gid { get; set; }


    public string Name { get; set; }
    public string Code { get; set; }
    public int Capacity { get; set; }
    public string? Description { get; set; }



    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, UpdatedRoomTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRoomTypeWriteRepository _roomTypeWriteRepository;
        private readonly IRoomTypeReadRepository _roomTypeReadRepository;
        private readonly RoomTypeBusinessRules _roomTypeBusinessRules;

        public UpdateRoomTypeCommandHandler(IMapper mapper, IRoomTypeWriteRepository roomTypeWriteRepository,
                                         RoomTypeBusinessRules roomTypeBusinessRules, IRoomTypeReadRepository roomTypeReadRepository)
        {
            _mapper = mapper;
            _roomTypeWriteRepository = roomTypeWriteRepository;
            _roomTypeBusinessRules = roomTypeBusinessRules;
            _roomTypeReadRepository = roomTypeReadRepository;
        }

        public async Task<UpdatedRoomTypeResponse> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            X.RoomType? roomType = await _roomTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _roomTypeBusinessRules.RoomTypeShouldExistWhenSelected(roomType);
            await _roomTypeBusinessRules.CheckRoomTypeNameIsUnique(request.Name, request.Gid);
            await _roomTypeBusinessRules.CheckRoomTypeCodeIsUnique(request.Code, request.Gid);
            roomType = _mapper.Map(request, roomType);

            _roomTypeWriteRepository.Update(roomType!);
            await _roomTypeWriteRepository.SaveAsync();
            GetByGidRoomTypeResponse obj = _mapper.Map<GetByGidRoomTypeResponse>(roomType);

            return new()
            {
                Title = RoomTypesBusinessMessages.ProcessCompleted,
                Message = RoomTypesBusinessMessages.SuccessCreatedRoomTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}