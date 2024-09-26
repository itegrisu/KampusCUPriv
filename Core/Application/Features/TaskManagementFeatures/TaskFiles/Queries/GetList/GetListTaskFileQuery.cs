using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.TaskManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetList;

public class GetListTaskFileQuery : IRequest<GetListResponse<GetListTaskFileListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTaskFileQueryHandler : IRequestHandler<GetListTaskFileQuery, GetListResponse<GetListTaskFileListItemDto>>
    {
        private readonly ITaskFileReadRepository _taskFileReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TaskFile, GetListTaskFileListItemDto> _noPagination;

        public GetListTaskFileQueryHandler(ITaskFileReadRepository taskFileReadRepository, IMapper mapper, NoPagination<X.TaskFile, GetListTaskFileListItemDto> noPagination)
        {
            _taskFileReadRepository = taskFileReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTaskFileListItemDto>> Handle(GetListTaskFileQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<TaskFile, object>>[]
                    {
                       x => x.UserFK,
                       x=> x.TaskFK
                    });

            IPaginate<X.TaskFile> taskFiles = await _taskFileReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: i => i.Include(i => i.TaskFK).Include(i => i.UserFK),
                 cancellationToken: cancellationToken
            );

            GetListResponse<GetListTaskFileListItemDto> response = _mapper.Map<GetListResponse<GetListTaskFileListItemDto>>(taskFiles);
            return response;
        }
    }
}