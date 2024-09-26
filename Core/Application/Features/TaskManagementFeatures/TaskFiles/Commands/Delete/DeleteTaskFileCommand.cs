using Application.Features.TaskManagementFeatures.TaskFiles.Constants;
using Application.Features.TaskManagementFeatures.TaskFiles.Rules;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Delete;

public class DeleteTaskFileCommand : IRequest<DeletedTaskFileResponse>
{
    //public int Gid { get; set; }
    public Guid Gid { get; set; }

    public class DeleteTaskFileCommandHandler : IRequestHandler<DeleteTaskFileCommand, DeletedTaskFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskFileReadRepository _taskFileReadRepository;
        private readonly ITaskFileWriteRepository _taskFileWriteRepository;
        private readonly TaskFileBusinessRules _taskFileBusinessRules;

        public DeleteTaskFileCommandHandler(IMapper mapper, ITaskFileReadRepository taskFileReadRepository,
                                         TaskFileBusinessRules taskFileBusinessRules, ITaskFileWriteRepository taskFileWriteRepository)
        {
            _mapper = mapper;
            _taskFileReadRepository = taskFileReadRepository;
            _taskFileBusinessRules = taskFileBusinessRules;
            _taskFileWriteRepository = taskFileWriteRepository;
        }

        public async Task<DeletedTaskFileResponse> Handle(DeleteTaskFileCommand request, CancellationToken cancellationToken)
        {
            await _taskFileBusinessRules.TaskFileShouldExistWhenSelected(request.Gid);

            X.TaskFile? taskFile = await _taskFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            taskFile.DataState = Core.Enum.DataState.Deleted;

            _taskFileWriteRepository.Update(taskFile);
            await _taskFileWriteRepository.SaveAsync();

            return new()
            {
                Title = TaskFilesBusinessMessages.ProcessCompleted,
                Message = TaskFilesBusinessMessages.SuccessDeletedTaskFileMessage,
                IsValid = true
            };
        }
    }
}