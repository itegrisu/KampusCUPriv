using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetList;

public class GetListPartTimeWorkerFileQuery : IRequest<GetListResponse<GetListPartTimeWorkerFileListItemDto>>
{
    public string PartTimeWorkerGid { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
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
            Expression<Func<X.PartTimeWorkerFile, bool>> predicate = null;

            if (request.PartTimeWorkerGid != null)
                predicate = x => x.GidPartTimeWorkerFK.ToString() == request.PartTimeWorkerGid;

            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate);

            IPaginate<X.PartTimeWorkerFile> partTimeWorkerFiles = await _partTimeWorkerFileReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                //include: x => x.Include(x => x.PartTimeWorkerFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPartTimeWorkerFileListItemDto> response = _mapper.Map<GetListResponse<GetListPartTimeWorkerFileListItemDto>>(partTimeWorkerFiles);
            return response;
        }
    }
}