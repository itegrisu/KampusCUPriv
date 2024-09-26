using Application.Features.TaskManagementFeatures.Tasks.Rules;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using AutoMapper;
using T = Domain.Entities.TaskManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Features.TaskManagementFeatures.Tasks.Constants;


namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Archive
{
    public class ArchivedTaskCommand : IRequest<ArchivedTaskResponse>
    {
        public Guid Gid { get; set; }
        public class ArchivedTaskCommandHandler : IRequestHandler<ArchivedTaskCommand, ArchivedTaskResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITaskReadRepository _taskReadRepository;
            private readonly ITaskWriteRepository _taskWriteRepository;
            private readonly TaskBusinessRules _taskBusinessRules;

            public ArchivedTaskCommandHandler(IMapper mapper, ITaskReadRepository taskReadRepository,
                                             TaskBusinessRules taskBusinessRules, ITaskWriteRepository taskWriteRepository)
            {
                _mapper = mapper;
                _taskReadRepository = taskReadRepository;
                _taskBusinessRules = taskBusinessRules;
                _taskWriteRepository = taskWriteRepository;
            }

            public async Task<ArchivedTaskResponse> Handle(ArchivedTaskCommand request, CancellationToken cancellationToken)
            {
                await _taskBusinessRules.TaskShouldExistWhenSelected(request.Gid);

                T.Task? task = await _taskReadRepository.GetAsync(predicate: t => t.Gid == request.Gid, cancellationToken: cancellationToken);

                task.DataState = Core.Enum.DataState.Archive;
                _taskWriteRepository.Update(task);
                await _taskWriteRepository.SaveAsync();

                return new()
                {
                    Title = TasksBusinessMessages.ProcessCompleted,
                    Message = TasksBusinessMessages.SuccessArchivedTaskMessage,
                    IsValid = true
                };
            }
        }
    }
}
