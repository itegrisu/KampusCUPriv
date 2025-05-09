using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;
using Application.Abstractions.Redis;

namespace Application.Features.DefinitionFeatures.Departments.Queries.GetList;

public class GetListDepartmentQuery : IRequest<GetListResponse<GetListDepartmentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListDepartmentQueryHandler : IRequestHandler<GetListDepartmentQuery, GetListResponse<GetListDepartmentListItemDto>>
    {
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Department, GetListDepartmentListItemDto> _noPagination;
        private readonly IRedisCacheService _redisCacheService;

        public GetListDepartmentQueryHandler(IDepartmentReadRepository departmentReadRepository, IMapper mapper, NoPagination<X.Department, GetListDepartmentListItemDto> noPagination, IRedisCacheService redisCacheService)
        {
            _departmentReadRepository = departmentReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _redisCacheService = redisCacheService;
        }

        public async Task<GetListResponse<GetListDepartmentListItemDto>> Handle(GetListDepartmentQuery request, CancellationToken cancellationToken)
        {
            string key = $"Departments_{request.PageRequest.PageIndex}_{request.PageRequest.PageSize}";

            // 2) Cache kontrolü
            var cached = await _redisCacheService.GetAsync<GetListResponse<GetListDepartmentListItemDto>>(key);
            if (cached is not null)
                return cached;

            // 3) Asýl veri sorgusu
            GetListResponse<GetListDepartmentListItemDto> response;
            if (request.PageRequest.PageIndex == -1)
            {
                response = await _noPagination.NoPaginationData(
                    cancellationToken,
                    orderBy: x => x.Name
                );
            }
            else
            {
                IPaginate<X.Department> departments = await _departmentReadRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    orderBy: x => x.OrderBy(o => o.Name)
                );
                response = _mapper.Map<GetListResponse<GetListDepartmentListItemDto>>(departments);
            }

            // 4) Sonuçlarý cache’e yaz
            await _redisCacheService.SetAsync(key, response, TimeSpan.FromMinutes(5));

            return response;
        }
    }
}