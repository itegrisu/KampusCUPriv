using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Rules;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid
{
    public class GetByGidRoomTypeQuery : IRequest<GetByGidRoomTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidRoomTypeQueryHandler : IRequestHandler<GetByGidRoomTypeQuery, GetByGidRoomTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IRoomTypeReadRepository _roomTypeReadRepository;
            private readonly RoomTypeBusinessRules _roomTypeBusinessRules;

            public GetByGidRoomTypeQueryHandler(IMapper mapper, IRoomTypeReadRepository roomTypeReadRepository, RoomTypeBusinessRules roomTypeBusinessRules)
            {
                _mapper = mapper;
                _roomTypeReadRepository = roomTypeReadRepository;
                _roomTypeBusinessRules = roomTypeBusinessRules;
            }

            public async Task<GetByGidRoomTypeResponse> Handle(GetByGidRoomTypeQuery request, CancellationToken cancellationToken)
            {
                X.RoomType? roomType = await _roomTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _roomTypeBusinessRules.RoomTypeShouldExistWhenSelected(roomType);

                GetByGidRoomTypeResponse response = _mapper.Map<GetByGidRoomTypeResponse>(roomType);
                return response;
            }
        }
    }
}