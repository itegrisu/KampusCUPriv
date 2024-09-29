using Application.Features.DefinitionManagementFeatures.RoomTypes.Constants;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Create;

public class CreateRoomTypeCommand : IRequest<CreatedRoomTypeResponse>
{

    public string Name { get; set; }
    public string Code { get; set; }
    public int Capacity { get; set; }
    public string? Description { get; set; }



    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, CreatedRoomTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRoomTypeWriteRepository _roomTypeWriteRepository;
        private readonly IRoomTypeReadRepository _roomTypeReadRepository;
        private readonly RoomTypeBusinessRules _roomTypeBusinessRules;

        public CreateRoomTypeCommandHandler(IMapper mapper, IRoomTypeWriteRepository roomTypeWriteRepository,
                                         RoomTypeBusinessRules roomTypeBusinessRules, IRoomTypeReadRepository roomTypeReadRepository)
        {
            _mapper = mapper;
            _roomTypeWriteRepository = roomTypeWriteRepository;
            _roomTypeBusinessRules = roomTypeBusinessRules;
            _roomTypeReadRepository = roomTypeReadRepository;
        }

        public async Task<CreatedRoomTypeResponse> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            await _roomTypeBusinessRules.CheckRoomTypeNameIsUnique(request.Name);
            await _roomTypeBusinessRules.CheckRoomTypeCodeIsUnique(request.Code);

            X.RoomType roomType = _mapper.Map<X.RoomType>(request);

            await _roomTypeWriteRepository.AddAsync(roomType);
            await _roomTypeWriteRepository.SaveAsync();

            X.RoomType savedRoomType = await _roomTypeReadRepository.GetAsync(predicate: x => x.Gid == roomType.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidRoomTypeResponse obj = _mapper.Map<GetByGidRoomTypeResponse>(savedRoomType);
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