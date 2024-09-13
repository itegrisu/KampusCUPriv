using Application.Helpers.PaginationHelpers;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetList;

public class GetListLogEmailSendQuery : IRequest<GetListResponse<GetListLogEmailSendListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListLogEmailSendQueryHandler : IRequestHandler<GetListLogEmailSendQuery, GetListResponse<GetListLogEmailSendListItemDto>>
    {
        private readonly ILogEmailSendReadRepository _logEmailSendReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.LogEmailSend, GetListLogEmailSendListItemDto> _noPagination;

        public GetListLogEmailSendQueryHandler(ILogEmailSendReadRepository logEmailSendReadRepository, IMapper mapper, NoPagination<X.LogEmailSend, GetListLogEmailSendListItemDto> noPagination)
        {
            _logEmailSendReadRepository = logEmailSendReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListLogEmailSendListItemDto>> Handle(GetListLogEmailSendQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<LogEmailSend, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.LogEmailSendMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.LogEmailSend> logEmailSends = await _logEmailSendReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLogEmailSendListItemDto> response = _mapper.Map<GetListResponse<GetListLogEmailSendListItemDto>>(logEmailSends);
            return response;
        }
    }
}