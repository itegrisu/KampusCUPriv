using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetList;

public class GetListTaskManagerQuery : IRequest<GetListResponse<GetListTaskManagerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTaskManagerQueryHandler : IRequestHandler<GetListTaskManagerQuery, GetListResponse<GetListTaskManagerListItemDto>>
    {
        private readonly ITaskManagerReadRepository _taskManagerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TaskManager, GetListTaskManagerListItemDto> _noPagination;

        public GetListTaskManagerQueryHandler(ITaskManagerReadRepository taskManagerReadRepository, IMapper mapper, NoPagination<X.TaskManager, GetListTaskManagerListItemDto> noPagination)
        {
            _taskManagerReadRepository = taskManagerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTaskManagerListItemDto>> Handle(GetListTaskManagerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<X.TaskManager, object>>[]
                    {
                       x => x.UserFK
                    });

            IPaginate<X.TaskManager> taskManagers = await _taskManagerReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: i => i.Include(i => i.UserFK)
            );

            GetListResponse<GetListTaskManagerListItemDto> response = _mapper.Map<GetListResponse<GetListTaskManagerListItemDto>>(taskManagers);
            return response;
        }
    }
}