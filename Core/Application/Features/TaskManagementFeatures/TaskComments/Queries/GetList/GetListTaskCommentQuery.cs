using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskComments.Queries.GetList;

public class GetListTaskCommentQuery : IRequest<GetListResponse<GetListTaskCommentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTaskCommentQueryHandler : IRequestHandler<GetListTaskCommentQuery, GetListResponse<GetListTaskCommentListItemDto>>
    {
        private readonly ITaskCommentReadRepository _taskCommentReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<TaskComment, GetListTaskCommentListItemDto> _noPagination;

        public GetListTaskCommentQueryHandler(ITaskCommentReadRepository taskCommentReadRepository, IMapper mapper, NoPagination<TaskComment, GetListTaskCommentListItemDto> noPagination)
        {
            _taskCommentReadRepository = taskCommentReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTaskCommentListItemDto>> Handle(GetListTaskCommentQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(
                    cancellationToken
                    //includes: new Expression<Func<T.Task, object>>[]
                    //{
                    //   x => x.,
                    //   x => x.UniversityFK
                    //}
                    );
            }

            IPaginate<TaskComment> taskComments = await _taskCommentReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTaskCommentListItemDto> response = _mapper.Map<GetListResponse<GetListTaskCommentListItemDto>>(taskComments);
            return response;
        }
    }
}