using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetList;

public class GetListPartTimeWorkerFileQuery : IRequest<GetListResponse<GetListPartTimeWorkerFileListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPartTimeWorkerFileQueryHandler : IRequestHandler<GetListPartTimeWorkerFileQuery, GetListResponse<GetListPartTimeWorkerFileListItemDto>>
    {
        private readonly IPartTimeWorkerFileReadRepository _partTimeWorkerFileReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PartTimeWorkerFile, GetListPartTimeWorkerFileListItemDto> _noPagination;

        public GetListPartTimeWorkerFileQueryHandler(IPartTimeWorkerFileReadRepository partTimeWorkerFileReadRepository, IMapper mapper, NoPagination<X.PartTimeWorkerFile, GetListPartTimeWorkerFileListItemDto> noPagination)
        {
            _partTimeWorkerFileReadRepository = partTimeWorkerFileReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPartTimeWorkerFileListItemDto>> Handle(GetListPartTimeWorkerFileQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<PartTimeWorkerFile, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.PartTimeWorkerFileMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.PartTimeWorkerFile> partTimeWorkerFiles = await _partTimeWorkerFileReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPartTimeWorkerFileListItemDto> response = _mapper.Map<GetListResponse<GetListPartTimeWorkerFileListItemDto>>(partTimeWorkerFiles);
            return response;
        }
    }
}