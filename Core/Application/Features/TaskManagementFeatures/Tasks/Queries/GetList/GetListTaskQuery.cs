using Application.Repositories.TaskManagementRepos.TaskRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using T = Domain.Entities.TaskManagements;
using MediatR;
using Application.Helpers.PaginationHelpers;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Enum;

namespace Application.Features.TaskManagementFeatures.Tasks.Queries.GetList;

public class GetListTaskQuery : IRequest<GetListResponse<GetListTaskListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTaskQueryHandler : IRequestHandler<GetListTaskQuery, GetListResponse<GetListTaskListItemDto>>
    {
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<T.Task, GetListTaskListItemDto> _noPagination;

        public GetListTaskQueryHandler(ITaskReadRepository taskReadRepository, IMapper mapper, NoPagination<T.Task, GetListTaskListItemDto> noPagination)
        {
            _taskReadRepository = taskReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTaskListItemDto>> Handle(GetListTaskQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationAllData(
                    cancellationToken,
                    predicate:x=>x.DataState == DataState.Active || x.DataState==DataState.Archive,
                    includes: new Expression<Func<T.Task, object>>[]
                    {
                       x => x.UserFK
                    }
                    );
            }

            IPaginate<T.Task> tasks = await _taskReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                predicate: x => x.DataState == DataState.Active || x.DataState == DataState.Archive,
                include: x => x.Include(x => x.UserFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTaskListItemDto> response = _mapper.Map<GetListResponse<GetListTaskListItemDto>>(tasks);
            return response;
        }
    }
}