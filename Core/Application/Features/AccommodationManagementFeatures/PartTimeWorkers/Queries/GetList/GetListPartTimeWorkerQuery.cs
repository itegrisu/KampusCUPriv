using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetList;

public class GetListPartTimeWorkerQuery : IRequest<GetListResponse<GetListPartTimeWorkerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPartTimeWorkerQueryHandler : IRequestHandler<GetListPartTimeWorkerQuery, GetListResponse<GetListPartTimeWorkerListItemDto>>
    {
        private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PartTimeWorker, GetListPartTimeWorkerListItemDto> _noPagination;

        public GetListPartTimeWorkerQueryHandler(IPartTimeWorkerReadRepository partTimeWorkerReadRepository, IMapper mapper, NoPagination<X.PartTimeWorker, GetListPartTimeWorkerListItemDto> noPagination)
        {
            _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPartTimeWorkerListItemDto>> Handle(GetListPartTimeWorkerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<PartTimeWorker, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.PartTimeWorkerMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.PartTimeWorker> partTimeWorkers = await _partTimeWorkerReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPartTimeWorkerListItemDto> response = _mapper.Map<GetListResponse<GetListPartTimeWorkerListItemDto>>(partTimeWorkers);
            return response;
        }
    }
}