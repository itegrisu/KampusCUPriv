using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupportManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetList;

public class GetListSupportMessageDetailQuery : IRequest<GetListResponse<GetListSupportMessageDetailListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSupportMessageDetailQueryHandler : IRequestHandler<GetListSupportMessageDetailQuery, GetListResponse<GetListSupportMessageDetailListItemDto>>
    {
        private readonly ISupportMessageDetailReadRepository _supportMessageDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SupportMessageDetail, GetListSupportMessageDetailListItemDto> _noPagination;

        public GetListSupportMessageDetailQueryHandler(ISupportMessageDetailReadRepository supportMessageDetailReadRepository, IMapper mapper, NoPagination<X.SupportMessageDetail, GetListSupportMessageDetailListItemDto> noPagination)
        {
            _supportMessageDetailReadRepository = supportMessageDetailReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSupportMessageDetailListItemDto>> Handle(GetListSupportMessageDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                  includes: new Expression<Func<SupportMessageDetail, object>>[]
                  {
                       x => x.UserFK,
                       x=> x.SupportMessageFK
                  });
            }

              
              IPaginate<X.SupportMessageDetail> supportMessageDetails = await _supportMessageDetailReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: i => i.Include(i => i.UserFK).Include(i => i.SupportMessageFK)
            );

            GetListResponse<GetListSupportMessageDetailListItemDto> response = _mapper.Map<GetListResponse<GetListSupportMessageDetailListItemDto>>(supportMessageDetails);
            return response;
        }
    }
}