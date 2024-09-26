using Application.Features.TaskManagementFeatures.TaskUsers.Rules;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TaskManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByUserGid
{
    public class GetByUserGidTaskUserQuery : IRequest<GetListResponse<GetByUserGidTaskUserResponse>>
    {
        public Guid UserGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetByUserGidTaskUserQueryHandler : IRequestHandler<GetByUserGidTaskUserQuery, GetListResponse<GetByUserGidTaskUserResponse>>
        {
            private readonly IMapper _mapper;
            private readonly ITaskUserReadRepository _taskUserReadRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly NoPagination<TaskUser, GetByUserGidTaskUserResponse> _noPagination;
            private readonly TaskUserBusinessRules _taskUserBusinessRules;
            public GetByUserGidTaskUserQueryHandler(IMapper mapper, NoPagination<TaskUser, GetByUserGidTaskUserResponse> noPagination, IUserReadRepository userReadRepository, ITaskUserReadRepository taskUserReadRepository, TaskUserBusinessRules taskUserBusinessRules)
            {
                _mapper = mapper;
                _noPagination = noPagination;
                _userReadRepository = userReadRepository;
                _taskUserReadRepository = taskUserReadRepository;
                _taskUserBusinessRules = taskUserBusinessRules;
            }

            public async Task<GetListResponse<GetByUserGidTaskUserResponse>> Handle(GetByUserGidTaskUserQuery request, CancellationToken cancellationToken)
            {
                await _taskUserBusinessRules.UserShouldExistWhenSelected(request.UserGid);

                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidUserFK == request.UserGid && x.TaskFK.DataState == Core.Enum.DataState.Active,
                   includes: new Expression<Func<TaskUser, object>>[]
                             {
                          x => x.UserFK,
                          x=>x.TaskFK
                        });
                }
                IPaginate<TaskUser> taskUser = await _taskUserReadRepository.GetListAsync(
               include: x => x.Include(x => x.UserFK).Include(x => x.TaskFK),
               index: request.PageIndex,
               size: request.PageSize,
               predicate: x => x.GidUserFK == request.UserGid && x.TaskFK.DataState == Core.Enum.DataState.Active,
               cancellationToken: cancellationToken
           );


                GetListResponse<GetByUserGidTaskUserResponse> response = _mapper.Map<GetListResponse<GetByUserGidTaskUserResponse>>(taskUser);

                return response;
            }
        }
    }
}
