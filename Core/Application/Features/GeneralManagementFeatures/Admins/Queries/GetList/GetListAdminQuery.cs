using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.GeneralManagementRepo.AdminRepo;

namespace Application.Features.GeneralFeatures.Admins.Queries.GetList;

public class GetListAdminQuery : IRequest<GetListResponse<GetListAdminListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAdminQueryHandler : IRequestHandler<GetListAdminQuery, GetListResponse<GetListAdminListItemDto>>
    {
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Admin, GetListAdminListItemDto> _noPagination;

        public GetListAdminQueryHandler(IAdminReadRepository adminReadRepository, IMapper mapper, NoPagination<X.Admin, GetListAdminListItemDto> noPagination)
        {
            _adminReadRepository = adminReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAdminListItemDto>> Handle(GetListAdminQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<Admin, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.AdminMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.Admin> admins = await _adminReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAdminListItemDto> response = _mapper.Map<GetListResponse<GetListAdminListItemDto>>(admins);
            return response;
        }
    }
}