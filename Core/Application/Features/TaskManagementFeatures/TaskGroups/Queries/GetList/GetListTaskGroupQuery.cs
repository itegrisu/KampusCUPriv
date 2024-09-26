using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetList;

public class GetListTaskGroupQuery : IRequest<GetListResponse<GetListTaskGroupListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTaskGroupQueryHandler : IRequestHandler<GetListTaskGroupQuery, GetListResponse<GetListTaskGroupListItemDto>>
    {
        private readonly ITaskGroupReadRepository _taskGroupReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TaskGroup, GetListTaskGroupListItemDto> _noPagination;

        public GetListTaskGroupQueryHandler(ITaskGroupReadRepository taskGroupReadRepository, IMapper mapper, NoPagination<X.TaskGroup, GetListTaskGroupListItemDto> noPagination)
        {
            _taskGroupReadRepository = taskGroupReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTaskGroupListItemDto>> Handle(GetListTaskGroupQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<TaskGroup, object>>[]
                    {
                       x => x.TaskGroupUsers
                    });

            IPaginate<X.TaskGroup> taskGroups = await _taskGroupReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: x => x.Include(x => x.TaskGroupUsers),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTaskGroupListItemDto> response = _mapper.Map<GetListResponse<GetListTaskGroupListItemDto>>(taskGroups);
            return response;
        }
    }
}