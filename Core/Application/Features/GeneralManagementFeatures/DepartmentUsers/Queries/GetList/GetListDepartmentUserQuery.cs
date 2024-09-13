using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetList;

public class GetListDepartmentUserQuery : IRequest<GetListResponse<GetListDepartmentUserListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListDepartmentUserQueryHandler : IRequestHandler<GetListDepartmentUserQuery, GetListResponse<GetListDepartmentUserListItemDto>>
    {
        private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.DepartmentUser, GetListDepartmentUserListItemDto> _noPagination;

        public GetListDepartmentUserQueryHandler(IDepartmentUserReadRepository departmentUserReadRepository, IMapper mapper, NoPagination<X.DepartmentUser, GetListDepartmentUserListItemDto> noPagination)
        {
            _departmentUserReadRepository = departmentUserReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListDepartmentUserListItemDto>> Handle(GetListDepartmentUserQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<DepartmentUser, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.DepartmentUserMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.DepartmentUser> departmentUsers = await _departmentUserReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDepartmentUserListItemDto> response = _mapper.Map<GetListResponse<GetListDepartmentUserListItemDto>>(departmentUsers);
            return response;
        }
    }
}