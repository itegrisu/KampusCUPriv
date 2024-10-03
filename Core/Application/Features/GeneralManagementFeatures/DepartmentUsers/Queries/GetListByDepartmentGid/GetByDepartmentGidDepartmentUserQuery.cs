using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetListByDepartmentGid;

public class GetByDepartmentGidDepartmentUserQuery : IRequest<GetListResponse<GetByDepartmentGidDepartmentUserListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public Guid GidDepartmentFK { get; set; }

    public class GetByDepartmentGidDepartmentUserQueryHandler : IRequestHandler<GetByDepartmentGidDepartmentUserQuery, GetListResponse<GetByDepartmentGidDepartmentUserListItemDto>>
    {
        private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.DepartmentUser, GetByDepartmentGidDepartmentUserListItemDto> _noPagination;

        public GetByDepartmentGidDepartmentUserQueryHandler(IDepartmentUserReadRepository departmentUserReadRepository, IMapper mapper, NoPagination<X.DepartmentUser, GetByDepartmentGidDepartmentUserListItemDto> noPagination)
        {
            _departmentUserReadRepository = departmentUserReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetByDepartmentGidDepartmentUserListItemDto>> Handle(GetByDepartmentGidDepartmentUserQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
            {

                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<DepartmentUser, object>>[]
                    {
                       x => x.UserFK,
                       x=> x.DepartmentFK,
                    },
                    predicate: x => x.GidDepartmentFK == request.GidDepartmentFK
                    );
            }

            IPaginate<X.DepartmentUser> departmentUsers = await _departmentUserReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                predicate: x => x.GidDepartmentFK == request.GidDepartmentFK
            );

            GetListResponse<GetByDepartmentGidDepartmentUserListItemDto> response = _mapper.Map<GetListResponse<GetByDepartmentGidDepartmentUserListItemDto>>(departmentUsers);
            return response;
        }
    }
}