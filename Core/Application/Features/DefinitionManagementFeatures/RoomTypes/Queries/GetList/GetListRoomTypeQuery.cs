using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetList;

public class GetListRoomTypeQuery : IRequest<GetListResponse<GetListRoomTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListRoomTypeQueryHandler : IRequestHandler<GetListRoomTypeQuery, GetListResponse<GetListRoomTypeListItemDto>>
    {
        private readonly IRoomTypeReadRepository _roomTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.RoomType, GetListRoomTypeListItemDto> _noPagination;

        public GetListRoomTypeQueryHandler(IRoomTypeReadRepository roomTypeReadRepository, IMapper mapper, NoPagination<X.RoomType, GetListRoomTypeListItemDto> noPagination)
        {
            _roomTypeReadRepository = roomTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListRoomTypeListItemDto>> Handle(GetListRoomTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<RoomType, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.RoomTypeMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.RoomType> roomTypes = await _roomTypeReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListRoomTypeListItemDto> response = _mapper.Map<GetListResponse<GetListRoomTypeListItemDto>>(roomTypes);
            return response;
        }
    }
}