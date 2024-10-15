using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.GeneralManagements;


namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetListWithUser
{
    public class GetListWithUserDepartmentQuery : IRequest<GetListResponse<GetListWithUserDepartmentListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListWithUserDepartmentQueryHandler : IRequestHandler<GetListWithUserDepartmentQuery, GetListResponse<GetListWithUserDepartmentListItemDto>>
        {
            private readonly IDepartmentReadRepository _departmentReadRepository;
            private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Department, GetListWithUserDepartmentListItemDto> _noPagination;

            public GetListWithUserDepartmentQueryHandler(IDepartmentReadRepository departmentReadRepository, IMapper mapper, NoPagination<X.Department, GetListWithUserDepartmentListItemDto> noPagination, IDepartmentUserReadRepository departmentUserReadRepository)
            {
                _departmentReadRepository = departmentReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _departmentUserReadRepository = departmentUserReadRepository;
            }

            public async Task<GetListResponse<GetListWithUserDepartmentListItemDto>> Handle(GetListWithUserDepartmentQuery request, CancellationToken cancellationToken)
            {
                if (request.PageRequest.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                                       includes: new Expression<Func<Department, object>>[]
                                           {
                            x => x.CoAdminFK,
                            x => x.MainAdminFK,
                            x => x.DepartmentUsers
                                       });
                }

                IPaginate<X.Department> departments = await _departmentReadRepository.GetListAsync(
                                       index: request.PageRequest.PageIndex,
                                       size: request.PageRequest.PageSize,
                                       cancellationToken: cancellationToken,
                                      include: X => X.Include(x => x.CoAdminFK).Include(x => x.MainAdminFK).Include(x => x.DepartmentUsers));

                GetListResponse<GetListWithUserDepartmentListItemDto> response = _mapper.Map<GetListResponse<GetListWithUserDepartmentListItemDto>>(departments);

                return response;

            }

        }
    }
}
