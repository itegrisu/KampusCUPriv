using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupportManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetList;

public class GetListSupportMessageQuery : IRequest<GetListResponse<GetListSupportMessageListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public Guid SupportGid { get; set; }

    public class GetListSupportMessageQueryHandler : IRequestHandler<GetListSupportMessageQuery, GetListResponse<GetListSupportMessageListItemDto>>
    {
        private readonly ISupportMessageReadRepository _supportMessageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SupportMessage, GetListSupportMessageListItemDto> _noPagination;

        public GetListSupportMessageQueryHandler(ISupportMessageReadRepository supportMessageReadRepository, IMapper mapper, NoPagination<X.SupportMessage, GetListSupportMessageListItemDto> noPagination)
        {
            _supportMessageReadRepository = supportMessageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSupportMessageListItemDto>> Handle(GetListSupportMessageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationAllData(cancellationToken,
                 includes: new Expression<Func<SupportMessage, object>>[]
                 {
                       x => x.UserFK,
                       x=> x.SupportRequestFK,
                       x=>x.SupportMessageDetails
                 },
                 predicate: x=>x.GidSupportFK == request.SupportGid
                 );
            }

             
           
            IPaginate<X.SupportMessage> supportMessages = await _supportMessageReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                include: X => X.Include(x => x.UserFK).Include(x => x.SupportRequestFK).Include(x=>x.SupportMessageDetails),
                 predicate: x => x.GidSupportFK == request.SupportGid
            );

            GetListResponse<GetListSupportMessageListItemDto> response = _mapper.Map<GetListResponse<GetListSupportMessageListItemDto>>(supportMessages);
            return response;
        }
    }
}