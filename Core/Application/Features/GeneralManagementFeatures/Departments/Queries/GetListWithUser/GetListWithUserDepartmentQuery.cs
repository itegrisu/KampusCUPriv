using Application.Features.GeneralManagementFeatures.Departments.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.GeneralManagements;


namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetListWithUser
{
    public class GetListWithUserDepartmentQuery : IRequest<GetListResponse<GetListWithUserDepartmentListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListWithUserDepartmentQueryHandler : IRequestHandler<GetListWithUserDepartmentQuery, GetListResponse<GetListWithUserDepartmentListItemDto>>
        {
            private readonly IDepartmentReadRepository _departmentReadRepository;

            private readonly IMapper _mapper;
            private readonly NoPagination<X.Department, GetListWithUserDepartmentListItemDto> _noPagination;

            public GetListWithUserDepartmentQueryHandler(IDepartmentReadRepository departmentReadRepository, IMapper mapper, NoPagination<X.Department, GetListWithUserDepartmentListItemDto> noPagination)
            {
                _departmentReadRepository = departmentReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
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
                       });
                }
                IPaginate<X.Department> departments = await _departmentReadRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    include: X => X.Include(x => x.CoAdminFK).Include(x => x.MainAdminFK)
                );

                GetListResponse<GetListWithUserDepartmentListItemDto> response = _mapper.Map<GetListResponse<GetListWithUserDepartmentListItemDto>>(departments);
                return response;
            }
        }
    }
}
