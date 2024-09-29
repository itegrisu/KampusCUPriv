using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.TaskManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetTaskCountList
{
    public class GetTaskCountListTaskUserQuery : IRequest<GetListResponse<GetTaskCountListTaskUserListItemDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public class GetTaskCountListTaskUserQueryHandler : IRequestHandler<GetTaskCountListTaskUserQuery, GetListResponse<GetTaskCountListTaskUserListItemDto>>
        {
            private readonly ITaskUserReadRepository _taskUserReadRepository;
            private readonly IMapper _mapper;

            public GetTaskCountListTaskUserQueryHandler(ITaskUserReadRepository taskUserReadRepository, IMapper mapper)
            {
                _taskUserReadRepository = taskUserReadRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetTaskCountListTaskUserListItemDto>> Handle(GetTaskCountListTaskUserQuery request, CancellationToken cancellationToken)
            {
                // Görevler listesi sorgusu
                List<TaskUser> taskUsers = _taskUserReadRepository.GetAll().Where(x => x.UserFK.DataState == Core.Enum.DataState.Active).Include(x => x.UserFK).Include(x => x.TaskFK).ToList();

                var taskUserListItems = new List<GetTaskCountListTaskUserListItemDto>();

                // Kullanıcıları gruplama
                var groupedTaskUsers = taskUsers
                    .GroupBy(x => x.GidUserFK)
                    .ToList();

                foreach (var group in groupedTaskUsers)
                {
                    // Her kullanıcı için görevlerin sayılması
                    var activeTaskCount = group.Count(t =>
                        t.TaskFK.EndDate > DateTime.Now && t.TaskState == EnumTaskState.Continues && t.TaskFK.DataState == Core.Enum.DataState.Active);

                    var delayedTaskCount = group.Count(t =>
                        t.TaskFK.EndDate <= DateTime.Now && t.TaskState == EnumTaskState.Continues && t.TaskFK.DataState == Core.Enum.DataState.Active);

                    var completedTaskCount = group.Count(t =>
                        t.TaskState == EnumTaskState.Completed && t.TaskFK.DataState == Core.Enum.DataState.Active);

                    // DTO oluşturma
                    var dto = new GetTaskCountListTaskUserListItemDto
                    {
                        GidUserFK = group.Key,
                        UserFKFullName = group.First().UserFK.FullName,
                        ActiveTaskCount = activeTaskCount,
                        DelayedTaskCount = delayedTaskCount,
                        CompletedTaskCount = completedTaskCount
                    };

                    taskUserListItems.Add(dto);
                }

                // Sonuç döndürme
                return new GetListResponse<GetTaskCountListTaskUserListItemDto>
                {
                    Items = taskUserListItems, // burada response bir liste olarak geliyor
                    Count = taskUserListItems.Count,
                    HasNext = false,
                    HasPrevious = false,
                    Index = 0,
                    Pages = 0,
                    Size = 100,
                };
            }
        }
    }
}
