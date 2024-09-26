using Application.Features.TaskManagementFeatures.TaskUsers.Rules;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;
using T = Domain.Entities.TaskManagements;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetListByTaskGid
{
    public class GetListByTaskGidTaskUserQuery : IRequest<GetListResponse<GetListByTaskGidTaskUserListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid TaskGid { get; set; }

        public class GetListByTaskGidTaskUserQueryHandler : IRequestHandler<GetListByTaskGidTaskUserQuery, GetListResponse<GetListByTaskGidTaskUserListItemDto>>
        {
            private readonly IMapper _mapper;
            private readonly ITaskUserReadRepository _taskUserReadRepository;
            private readonly TaskUserBusinessRules _taskUserBusinessRules;
            private readonly ITaskReadRepository _taskReadRepository;
            private readonly NoPagination<TaskUser, GetListByTaskGidTaskUserListItemDto> _noPagination;

            public GetListByTaskGidTaskUserQueryHandler(IMapper mapper, ITaskUserReadRepository taskUserReadRepository, TaskUserBusinessRules taskUserBusinessRules, ITaskReadRepository taskReadRepository, NoPagination<TaskUser, GetListByTaskGidTaskUserListItemDto> noPagination)
            {
                _mapper = mapper;
                _taskUserReadRepository = taskUserReadRepository;
                _taskUserBusinessRules = taskUserBusinessRules;
                _taskReadRepository = taskReadRepository;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetListByTaskGidTaskUserListItemDto>> Handle(GetListByTaskGidTaskUserQuery request, CancellationToken cancellationToken)
            {
                await _taskUserBusinessRules.TaskShouldExistWhenSelected(request.TaskGid);

                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTaskFK == request.TaskGid,
                   includes: new Expression<Func<T.TaskUser, object>>[]
                             {
               x => x.TaskFK,
               x=>x.UserFK
                        });
                }
                IPaginate<TaskUser> taskUser = await _taskUserReadRepository.GetListAllAsync(
               include: x => x.Include(x => x.UserFK).Include(x => x.TaskFK),
               index: request.PageIndex,
               size: request.PageSize,
               predicate: x => x.GidTaskFK == request.TaskGid,
               cancellationToken: cancellationToken
           );

                GetListResponse<GetListByTaskGidTaskUserListItemDto> response = _mapper.Map<GetListResponse<GetListByTaskGidTaskUserListItemDto>>(taskUser);
                return response;
            }

        }
    }
}