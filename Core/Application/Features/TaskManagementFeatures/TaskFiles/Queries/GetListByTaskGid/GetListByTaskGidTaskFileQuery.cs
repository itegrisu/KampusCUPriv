using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;
using X = Domain.Entities.TaskManagements;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Application.Features.TaskManagementFeatures.TaskFiles.Rules;
using Application.Repositories.TaskManagementRepos.TaskRepo;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetListByTaskGid
{
    public class GetListByTaskGidTaskFileQuery : IRequest<GetListResponse<GetListByTaskGidTaskFileListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid TaskGid { get; set; }


        public class GetListByTaskGidTaskFileQueryHandler : IRequestHandler<GetListByTaskGidTaskFileQuery, GetListResponse<GetListByTaskGidTaskFileListItemDto>>
        {
            private readonly ITaskFileReadRepository _taskFileReadRepository;
            private readonly IMapper _mapper;
            private readonly ITaskReadRepository _taskReadRepository;
            private readonly TaskFileBusinessRules _taskFileBusinessRules;
            private readonly NoPagination<X.TaskFile, GetListByTaskGidTaskFileListItemDto> _noPagination;

            public GetListByTaskGidTaskFileQueryHandler(ITaskFileReadRepository taskFileReadRepository, IMapper mapper, NoPagination<X.TaskFile, GetListByTaskGidTaskFileListItemDto> noPagination, ITaskReadRepository taskReadRepository, TaskFileBusinessRules taskFileBusinessRules)
            {
                _taskFileReadRepository = taskFileReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _taskReadRepository = taskReadRepository;
                _taskFileBusinessRules = taskFileBusinessRules;
            }

            public async Task<GetListResponse<GetListByTaskGidTaskFileListItemDto>> Handle(GetListByTaskGidTaskFileQuery request, CancellationToken cancellationToken)
            {
                await _taskFileBusinessRules.TaskShouldExistWhenSelected(request.TaskGid);

                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.TaskFK.Gid == request.TaskGid,
                        includes: new Expression<Func<TaskFile, object>>[]
                        {
                       x => x.UserFK,
                       x=> x.TaskFK
                        });

                IPaginate<X.TaskFile> taskFiles = await _taskFileReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    predicate: x => x.TaskFK.Gid == request.TaskGid,
                    include: i => i.Include(i => i.TaskFK).Include(i => i.UserFK),
                     cancellationToken: cancellationToken
                );

                GetListResponse<GetListByTaskGidTaskFileListItemDto> response = _mapper.Map<GetListResponse<GetListByTaskGidTaskFileListItemDto>>(taskFiles);
                return response;
            }
        }
    }
}