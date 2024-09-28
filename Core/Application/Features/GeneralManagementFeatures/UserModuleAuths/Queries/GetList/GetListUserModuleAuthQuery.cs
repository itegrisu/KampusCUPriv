using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetList;

public class GetListUserModuleAuthQuery : IRequest<GetListResponse<GetListUserModuleAuthListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserModuleAuthQueryHandler : IRequestHandler<GetListUserModuleAuthQuery, GetListResponse<GetListUserModuleAuthListItemDto>>
    {
        private readonly IUserModuleAuthReadRepository _userModuleAuthReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.UserModuleAuth, GetListUserModuleAuthListItemDto> _noPagination;

        public GetListUserModuleAuthQueryHandler(IUserModuleAuthReadRepository userModuleAuthReadRepository, IMapper mapper, NoPagination<X.UserModuleAuth, GetListUserModuleAuthListItemDto> noPagination)
        {
            _userModuleAuthReadRepository = userModuleAuthReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListUserModuleAuthListItemDto>> Handle(GetListUserModuleAuthQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<UserModuleAuth, object>>[]
                    {
                       x => x.UserFK,
                    });

            }
            IPaginate<X.UserModuleAuth> userModuleAuths = await _userModuleAuthReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK)
            );

            GetListResponse<GetListUserModuleAuthListItemDto> response = _mapper.Map<GetListResponse<GetListUserModuleAuthListItemDto>>(userModuleAuths);
            return response;
        }
    }
}