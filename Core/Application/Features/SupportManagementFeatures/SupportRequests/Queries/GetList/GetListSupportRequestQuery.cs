using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupportManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetList;

public class GetListSupportRequestQuery : IRequest<GetListResponse<GetListSupportRequestListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string UserGid { get; set; }

    public class GetListSupportRequestQueryHandler : IRequestHandler<GetListSupportRequestQuery, GetListResponse<GetListSupportRequestListItemDto>>
    {
        private readonly ISupportRequestReadRepository _supportRequestReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SupportRequest, GetListSupportRequestListItemDto> _noPagination;

        public GetListSupportRequestQueryHandler(ISupportRequestReadRepository supportRequestReadRepository, IMapper mapper, NoPagination<X.SupportRequest, GetListSupportRequestListItemDto> noPagination)
        {
            _supportRequestReadRepository = supportRequestReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSupportRequestListItemDto>> Handle(GetListSupportRequestQuery request, CancellationToken cancellationToken)
        {


            Expression<Func<SupportRequest, bool>> predicate;
            if (request.UserGid == "0" || request.UserGid == null)
            {
                predicate = x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime;
            }
            else
            {
                predicate = x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.CreatedUserFK.ToString() == request.UserGid ;
            }


            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<SupportRequest, object>>[]
                    {
                       x => x.UserFK,
                    },
                    predicate: predicate);
            }
                
           
            IPaginate<X.SupportRequest> supportRequests = await _supportRequestReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                include: i=>i.Include(x => x.UserFK),
                predicate: predicate);

            GetListResponse<GetListSupportRequestListItemDto> response = _mapper.Map<GetListResponse<GetListSupportRequestListItemDto>>(supportRequests);
            return response;
        }
    }
}