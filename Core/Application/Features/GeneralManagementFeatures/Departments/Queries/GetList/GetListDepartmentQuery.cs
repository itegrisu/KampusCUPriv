using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetList;

public class GetListDepartmentQuery : IRequest<GetListResponse<GetListDepartmentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListDepartmentQueryHandler : IRequestHandler<GetListDepartmentQuery, GetListResponse<GetListDepartmentListItemDto>>
    {
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Department, GetListDepartmentListItemDto> _noPagination;

        public GetListDepartmentQueryHandler(IDepartmentReadRepository departmentReadRepository, IMapper mapper, NoPagination<X.Department, GetListDepartmentListItemDto> noPagination)
        {
            _departmentReadRepository = departmentReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListDepartmentListItemDto>> Handle(GetListDepartmentQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<Department, object>>[]
                   {
                       x => x.YedekYoneticiFK,
                       x => x.AsilYoneticFK,
                   });
            }
            IPaginate<X.Department> departments = await _departmentReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: X => X.Include(x => x.YedekYoneticiFK).Include(x => x.AsilYoneticFK)
            );

            GetListResponse<GetListDepartmentListItemDto> response = _mapper.Map<GetListResponse<GetListDepartmentListItemDto>>(departments);
            return response;
        }
    }
}