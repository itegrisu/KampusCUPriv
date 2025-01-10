using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetList;

public class GetListAnnouncementTypeQuery : IRequest<GetListResponse<GetListAnnouncementTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAnnouncementTypeQueryHandler : IRequestHandler<GetListAnnouncementTypeQuery, GetListResponse<GetListAnnouncementTypeListItemDto>>
    {
        private readonly IAnnouncementTypeReadRepository _announcementTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.AnnouncementType, GetListAnnouncementTypeListItemDto> _noPagination;

        public GetListAnnouncementTypeQueryHandler(IAnnouncementTypeReadRepository announcementTypeReadRepository, IMapper mapper, NoPagination<X.AnnouncementType, GetListAnnouncementTypeListItemDto> noPagination)
        {
            _announcementTypeReadRepository = announcementTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAnnouncementTypeListItemDto>> Handle(GetListAnnouncementTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<AnnouncementType, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.AnnouncementTypeMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.AnnouncementType> announcementTypes = await _announcementTypeReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAnnouncementTypeListItemDto> response = _mapper.Map<GetListResponse<GetListAnnouncementTypeListItemDto>>(announcementTypes);
            return response;
        }
    }
}