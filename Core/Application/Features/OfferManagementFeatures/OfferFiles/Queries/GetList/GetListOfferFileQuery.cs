using Application.Helpers.PaginationHelpers;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OfferManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetList;

public class GetListOfferFileQuery : IRequest<GetListResponse<GetListOfferFileListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOfferFileQueryHandler : IRequestHandler<GetListOfferFileQuery, GetListResponse<GetListOfferFileListItemDto>>
    {
        private readonly IOfferFileReadRepository _offerFileReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OfferFile, GetListOfferFileListItemDto> _noPagination;

        public GetListOfferFileQueryHandler(IOfferFileReadRepository offerFileReadRepository, IMapper mapper, NoPagination<X.OfferFile, GetListOfferFileListItemDto> noPagination)
        {
            _offerFileReadRepository = offerFileReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOfferFileListItemDto>> Handle(GetListOfferFileQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                  includes: new Expression<Func<OfferFile, object>>[]
                  {
                       X=>X.OfferFK
                  });
            }

            IPaginate<X.OfferFile> offerFiles = await _offerFileReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.OfferFK)
            );

            GetListResponse<GetListOfferFileListItemDto> response = _mapper.Map<GetListResponse<GetListOfferFileListItemDto>>(offerFiles);
            return response;
        }
    }
}