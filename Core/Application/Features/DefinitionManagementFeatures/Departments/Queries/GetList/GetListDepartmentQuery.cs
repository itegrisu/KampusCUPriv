using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;

namespace Application.Features.DefinitionFeatures.Departments.Queries.GetList;

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
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<Department, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.DepartmentMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken,orderBy: x => x.Name);
            IPaginate<X.Department> departments = await _departmentReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderBy(o => o.Name)
            );

            GetListResponse<GetListDepartmentListItemDto> response = _mapper.Map<GetListResponse<GetListDepartmentListItemDto>>(departments);
            return response;
        }
    }
}