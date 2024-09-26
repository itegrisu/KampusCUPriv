using Application.Features.TaskManagementFeatures.TaskFiles.Constants;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskFiles.Rules;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using AutoMapper;
using X = Domain.Entities.TaskManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Update;

public class UpdateTaskFileCommand : IRequest<UpdatedTaskFileResponse>
{
    public Guid Gid { get; set; }
    public Guid GidFileUploadUserFK { get; set; }
    public Guid GidTaskFK { get; set; }


    public string FileTitle { get; set; }
    public string? FileDescription { get; set; }
    public string? UploadedFile { get; set; }



    public class UpdateTaskFileCommandHandler : IRequestHandler<UpdateTaskFileCommand, UpdatedTaskFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskFileWriteRepository _taskFileWriteRepository;
        private readonly ITaskFileReadRepository _taskFileReadRepository;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly TaskFileBusinessRules _taskFileBusinessRules;

        public UpdateTaskFileCommandHandler(IMapper mapper, ITaskFileWriteRepository taskFileWriteRepository,
                                         TaskFileBusinessRules taskFileBusinessRules, ITaskFileReadRepository taskFileReadRepository, ITaskReadRepository taskReadRepository, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _taskFileWriteRepository = taskFileWriteRepository;
            _taskFileBusinessRules = taskFileBusinessRules;
            _taskFileReadRepository = taskFileReadRepository;
            _taskReadRepository = taskReadRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<UpdatedTaskFileResponse> Handle(UpdateTaskFileCommand request, CancellationToken cancellationToken)
        {
            await _taskFileBusinessRules.TaskFileShouldExistWhenSelected(request.Gid);
            await _taskFileBusinessRules.UserShouldExistWhenSelected(request.GidFileUploadUserFK);
            await _taskFileBusinessRules.TaskShouldExistWhenSelected(request.GidTaskFK);

            X.TaskFile? taskFile = await _taskFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                include: i => i.Include(i => i.TaskFK).Include(i => i.UserFK));


            taskFile = _mapper.Map(request, taskFile);
            _taskFileWriteRepository.Update(taskFile!);
            await _taskFileWriteRepository.SaveAsync();
            GetByGidTaskFileResponse obj = _mapper.Map<GetByGidTaskFileResponse>(taskFile);

            return new()
            {
                Title = TaskFilesBusinessMessages.ProcessCompleted,
                Message = TaskFilesBusinessMessages.SuccessUpdatedTaskFileMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}