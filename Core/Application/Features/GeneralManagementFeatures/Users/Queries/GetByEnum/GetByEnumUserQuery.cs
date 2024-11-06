using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetByEnum
{
    public class GetByEnumUserQuery : IRequest<GetListResponse<GetByEnumUserListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public EnumWorkType WorkType { get; set; }
        public class GetByEnumUserQueryHandler : IRequestHandler<GetByEnumUserQuery, GetListResponse<GetByEnumUserListItemDto>>
        {
            private readonly NoPagination<User, GetByEnumUserListItemDto> _noPagination;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public GetByEnumUserQueryHandler(NoPagination<User, GetByEnumUserListItemDto> noPagination, IUserReadRepository userReadRepository, IMapper mapper)
            {
                _noPagination = noPagination;
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetByEnumUserListItemDto>> Handle(GetByEnumUserQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationAllData(
                        cancellationToken,
                        predicate:x => x.WorkType == request.WorkType && x.IsActive == true,
                        includes: new Expression<Func<User, object>>[]
                        {
                         x => x.CountryFK,
                        });
                }

                IPaginate<User> userCustoms = await _userReadRepository.GetListAllAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.WorkType == request.WorkType && x.IsActive == true

                );

                GetListResponse<GetByEnumUserListItemDto> response = _mapper.Map<GetListResponse<GetByEnumUserListItemDto>>(userCustoms);
                return response;
            }
        }
    }

}
