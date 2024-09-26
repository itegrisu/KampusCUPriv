using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;
using T = Domain.Entities.TaskManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Features.TaskManagementFeatures.TaskUsers.Rules;
using Microsoft.EntityFrameworkCore;
using Application.Features.TaskManagementFeatures.TaskComments.Rules;

namespace Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByTaskGid
{
    public class GetByTaskGidTaskCommentQuery : IRequest<GetListResponse<GetByTaskGidTaskCommentResponse>>
    {
        public Guid TaskGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetByTaskGidTaskCommentQueryHandler : IRequestHandler<GetByTaskGidTaskCommentQuery, GetListResponse<GetByTaskGidTaskCommentResponse>>
        {
            private readonly IMapper _mapper;
            private readonly ITaskCommentReadRepository _taskCommentReadRepository;
            private readonly ITaskReadRepository _taskReadRepository;
            private readonly ITaskUserReadRepository _taskUserReadRepository;
            NoPagination<TaskComment, GetByTaskGidTaskCommentResponse> _noPagination;
            private readonly TaskUserBusinessRules _taskUserBusinessRules;
            private readonly TaskCommentBusinessRules _taskCommentBusinessRules;

            public GetByTaskGidTaskCommentQueryHandler(IMapper mapper, ITaskCommentReadRepository taskCommentReadRepository, ITaskReadRepository taskReadRepository, NoPagination<TaskComment, GetByTaskGidTaskCommentResponse> noPagination, ITaskUserReadRepository taskUserReadRepository, TaskCommentBusinessRules taskCommentBusinessRules)
            {
                _mapper = mapper;
                _taskCommentReadRepository = taskCommentReadRepository;
                _taskReadRepository = taskReadRepository;
                _noPagination = noPagination;
                _taskUserReadRepository = taskUserReadRepository;
                _taskCommentBusinessRules = taskCommentBusinessRules;
            }

            public async Task<GetListResponse<GetByTaskGidTaskCommentResponse>> Handle(GetByTaskGidTaskCommentQuery request, CancellationToken cancellationToken)
            {
                await _taskCommentBusinessRules.TaskShouldExistWhenSelected(request.TaskGid);

                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTaskFK == request.TaskGid,
                        includes: new Expression<Func<TaskComment, object>>[]
                         {
                          x => x.UserFK,
                          x=>x.TaskFK
                        } // en yak?n tarihi getirir


                         );
                }
                IPaginate<TaskComment> taskComment = await _taskCommentReadRepository.GetListAsync(
                     predicate: x => x.GidTaskFK == request.TaskGid,
               include: x => x.Include(x => x.UserFK).Include(x => x.TaskFK),
               index: request.PageIndex,
               size: request.PageSize,
               cancellationToken: cancellationToken
           );


                GetListResponse<GetByTaskGidTaskCommentResponse> response = _mapper.Map<GetListResponse<GetByTaskGidTaskCommentResponse>>(taskComment);

                return response;
            }
        }
    }
}
