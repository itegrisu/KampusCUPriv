using Application.Helpers.PaginationHelpers;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetList;

public class GetListLogFailedLoginQuery : IRequest<GetListResponse<GetListLogFailedLoginListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Email { get; set; }

    public class GetListLogFailedLoginQueryHandler : IRequestHandler<GetListLogFailedLoginQuery, GetListResponse<GetListLogFailedLoginListItemDto>>
    {
        private readonly ILogFailedLoginReadRepository _logFailedLoginReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.LogFailedLogin, GetListLogFailedLoginListItemDto> _noPagination;

        public GetListLogFailedLoginQueryHandler(ILogFailedLoginReadRepository logFailedLoginReadRepository, IMapper mapper, NoPagination<X.LogFailedLogin, GetListLogFailedLoginListItemDto> noPagination)
        {
            _logFailedLoginReadRepository = logFailedLoginReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListLogFailedLoginListItemDto>> Handle(GetListLogFailedLoginQuery request, CancellationToken cancellationToken)
        {
            IPaginate<X.LogFailedLogin> logFailedLogins;
            GetListResponse<GetListLogFailedLoginListItemDto> response;
            if (request.Email == null || request.Email == "0")
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                                               predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime);
                }
                    
                logFailedLogins = await _logFailedLoginReadRepository.GetListAllAsync(
                                       index: request.PageIndex,
                                       size: request.PageSize,
                                       cancellationToken: cancellationToken,
                                       predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime);
                response = _mapper.Map<GetListResponse<GetListLogFailedLoginListItemDto>>(logFailedLogins);
            }
            else
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                                   predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.Email == request.Email );
                }
                   
                logFailedLogins = await _logFailedLoginReadRepository.GetListAllAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.Email == request.Email
                );

                response = _mapper.Map<GetListResponse<GetListLogFailedLoginListItemDto>>(logFailedLogins);
            }

           
            return response;
        }
    }
}