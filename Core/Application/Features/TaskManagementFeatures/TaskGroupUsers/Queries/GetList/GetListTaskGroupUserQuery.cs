using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetList;

public class GetListTaskGroupUserQuery : IRequest<GetListResponse<GetListTaskGroupUserListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTaskGroupUserQueryHandler : IRequestHandler<GetListTaskGroupUserQuery, GetListResponse<GetListTaskGroupUserListItemDto>>
    {
        private readonly ITaskGroupUserReadRepository _taskGroupUserReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TaskGroupUser, GetListTaskGroupUserListItemDto> _noPagination;

        public GetListTaskGroupUserQueryHandler(ITaskGroupUserReadRepository taskGroupUserReadRepository, IMapper mapper, NoPagination<X.TaskGroupUser, GetListTaskGroupUserListItemDto> noPagination)
        {
            _taskGroupUserReadRepository = taskGroupUserReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTaskGroupUserListItemDto>> Handle(GetListTaskGroupUserQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<TaskGroupUser, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.TaskGroupUserMembers
                //    });
				//TODO
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.TaskGroupUser> taskGroupUsers = await _taskGroupUserReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTaskGroupUserListItemDto> response = _mapper.Map<GetListResponse<GetListTaskGroupUserListItemDto>>(taskGroupUsers);
            return response;
        }
    }
}