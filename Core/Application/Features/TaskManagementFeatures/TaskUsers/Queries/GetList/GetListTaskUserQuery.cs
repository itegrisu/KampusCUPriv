using Application.Helpers.PaginationHelpers;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetList;

public class GetListTaskUserQuery : IRequest<GetListResponse<GetListTaskUserListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTaskUserQueryHandler : IRequestHandler<GetListTaskUserQuery, GetListResponse<GetListTaskUserListItemDto>>
    {
        private readonly ITaskUserReadRepository _taskUserReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TaskUser, GetListTaskUserListItemDto> _noPagination;

        public GetListTaskUserQueryHandler(ITaskUserReadRepository taskUserReadRepository, IMapper mapper, NoPagination<X.TaskUser, GetListTaskUserListItemDto> noPagination)
        {
            _taskUserReadRepository = taskUserReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTaskUserListItemDto>> Handle(GetListTaskUserQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<TaskUser, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.TaskUserMembers
                //    });
				//TODO
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.TaskUser> taskUsers = await _taskUserReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTaskUserListItemDto> response = _mapper.Map<GetListResponse<GetListTaskUserListItemDto>>(taskUsers);
            return response;
        }
    }
}