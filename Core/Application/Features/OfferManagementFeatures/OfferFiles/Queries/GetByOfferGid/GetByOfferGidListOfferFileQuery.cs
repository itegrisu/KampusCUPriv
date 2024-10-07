using Application.Helpers.PaginationHelpers;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OfferManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid
{
    public class GetByOfferGidListOfferFileQuery : IRequest<GetListResponse<GetByOfferGidListOfferFileListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid gid { get; set; }

        public class GetByOfferGidListOfferFileQueryHandler : IRequestHandler<GetByOfferGidListOfferFileQuery, GetListResponse<GetByOfferGidListOfferFileListItemDto>>
        {
            private readonly IOfferFileReadRepository _offerFileReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<OfferFile, GetByOfferGidListOfferFileListItemDto> _noPagination;

            public GetByOfferGidListOfferFileQueryHandler(IOfferFileReadRepository offerFileReadRepository, IMapper mapper, NoPagination<OfferFile, GetByOfferGidListOfferFileListItemDto> noPagination)
            {
                _offerFileReadRepository = offerFileReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByOfferGidListOfferFileListItemDto>> Handle(GetByOfferGidListOfferFileQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                      predicate: x => x.GidOfferFK == request.gid,
                      includes: new Expression<Func<OfferFile, object>>[]
                      {
                        X=>X.OfferFK
                      });
                }

                IPaginate<OfferFile> offerFiles = await _offerFileReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidOfferFK == request.gid,
                    include: x => x.Include(x => x.OfferFK)
                );

                GetListResponse<GetByOfferGidListOfferFileListItemDto> response = _mapper.Map<GetListResponse<GetByOfferGidListOfferFileListItemDto>>(offerFiles);
                return response;
            }
        }
    }
}
