using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Application.Features.TaskManagementFeatures.TaskComments.Rules;

namespace Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByTaskUserGid
{
    public class GetByTaskUserGidTaskCommentQuery : IRequest<GetListResponse<GetByTaskUserGidTaskCommentResponse>>
    {
        public Guid TaskUserGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetByTaskUserGidTaskCommentQueryHandler : IRequestHandler<GetByTaskUserGidTaskCommentQuery, GetListResponse<GetByTaskUserGidTaskCommentResponse>>
        {
            private readonly IMapper _mapper;
            private readonly ITaskCommentReadRepository _taskCommentReadRepository;
            private readonly ITaskUserReadRepository _taskUserReadRepository;
            private readonly ITaskReadRepository _taskReadRepository;
            NoPagination<TaskComment, GetByTaskUserGidTaskCommentResponse> _noPagination;
            private readonly TaskCommentBusinessRules _taskCommentBusinessRules;
            public GetByTaskUserGidTaskCommentQueryHandler(IMapper mapper, ITaskCommentReadRepository taskCommentReadRepository, ITaskReadRepository taskReadRepository, NoPagination<TaskComment, GetByTaskUserGidTaskCommentResponse> noPagination, ITaskUserReadRepository taskUserReadRepository, TaskCommentBusinessRules taskCommentBusinessRules)
            {
                _mapper = mapper;
                _taskCommentReadRepository = taskCommentReadRepository;
                _taskReadRepository = taskReadRepository;
                _noPagination = noPagination;
                _taskUserReadRepository = taskUserReadRepository;
                _taskCommentBusinessRules = taskCommentBusinessRules;
            }

            public async Task<GetListResponse<GetByTaskUserGidTaskCommentResponse>> Handle(GetByTaskUserGidTaskCommentQuery request, CancellationToken cancellationToken)
            {
                await _taskCommentBusinessRules.TaskUserShouldExistWhenSelected(request.TaskUserGid);

                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidUserFK == request.TaskUserGid,
                        includes: new Expression<Func<TaskComment, object>>[]
                         {
                          x => x.UserFK,
                          x=>x.TaskFK
                        } // en yakýn tarihi getirir
                         );
                }

                IPaginate<TaskComment> taskComment = await _taskCommentReadRepository.GetListAsync(
                    predicate: x => x.GidUserFK == request.TaskUserGid,
               include: x => x.Include(x => x.UserFK).Include(x => x.TaskFK),
               index: request.PageIndex,
               size: request.PageSize,
               cancellationToken: cancellationToken
           );


                GetListResponse<GetByTaskUserGidTaskCommentResponse> response = _mapper.Map<GetListResponse<GetByTaskUserGidTaskCommentResponse>>(taskComment);

                return response;
            }
        }
    }
}
