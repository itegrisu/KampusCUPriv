using Application.Features.TaskManagementFeatures.TaskGroupUsers.Rules;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGroupGid
{
    public class GetByGroupGidTaskGroupUserQuery : IRequest<GetListResponse<GetByGroupGidTaskGroupUserResponse>>
    {
        public Guid TaskGroupGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public class GetByGroupGidTaskGroupUserQueryHandler : IRequestHandler<GetByGroupGidTaskGroupUserQuery, GetListResponse<GetByGroupGidTaskGroupUserResponse>>
        {
            private readonly IMapper _mapper;
            private readonly ITaskGroupUserReadRepository _taskGroupUserReadRepository;
            private readonly TaskGroupUserBusinessRules _taskGroupUserBusinessRules;
            private readonly ITaskGroupReadRepository _taskGroupReadRepository;
            private readonly NoPagination<TaskGroupUser, GetByGroupGidTaskGroupUserResponse> _noPagination;

            public GetByGroupGidTaskGroupUserQueryHandler(IMapper mapper, ITaskGroupUserReadRepository taskGroupUserReadRepository, TaskGroupUserBusinessRules taskGroupUserBusinessRules, NoPagination<TaskGroupUser, GetByGroupGidTaskGroupUserResponse> noPagination, ITaskGroupReadRepository taskGroupReadRepository)
            {
                _mapper = mapper;
                _taskGroupUserReadRepository = taskGroupUserReadRepository;
                _taskGroupUserBusinessRules = taskGroupUserBusinessRules;
                _noPagination = noPagination;
                _taskGroupReadRepository = taskGroupReadRepository;
            }

            public async Task<GetListResponse<GetByGroupGidTaskGroupUserResponse>> Handle(GetByGroupGidTaskGroupUserQuery request, CancellationToken cancellationToken)
            {
                await _taskGroupUserBusinessRules.TaskGroupShouldExistWhenSelected(request.TaskGroupGid);

                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTaskGroupFK == request.TaskGroupGid,
                   includes: new Expression<Func<TaskGroupUser, object>>[]
                             {
                            x => x.UserFK,
                            x => x.TaskGroupFK,
                        });
                }

                IPaginate<TaskGroupUser> taskGroupUsers = await _taskGroupUserReadRepository.GetListAllAsync(
               include: x => x.Include(x => x.UserFK).Include(x => x.TaskGroupFK),
               index: request.PageIndex,
               size: request.PageSize,
               cancellationToken: cancellationToken
           );


                GetListResponse<GetByGroupGidTaskGroupUserResponse> response = _mapper.Map<GetListResponse<GetByGroupGidTaskGroupUserResponse>>(taskGroupUsers);
                return response;
            }
        }
    }
}