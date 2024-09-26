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

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Create;

public class CreateTaskFileCommand : IRequest<CreatedTaskFileResponse>
{
    public Guid GidFileUploadUserFK { get; set; }
    public Guid GidTaskFK { get; set; }

    public string FileTitle { get; set; }
    public string? FileDescription { get; set; }
    public string? UploadedFile { get; set; }



    public class CreateTaskFileCommandHandler : IRequestHandler<CreateTaskFileCommand, CreatedTaskFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITaskFileWriteRepository _taskFileWriteRepository;
        private readonly ITaskFileReadRepository _taskFileReadRepository;
        private readonly ITaskReadRepository _taskReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly TaskFileBusinessRules _taskFileBusinessRules;

        public CreateTaskFileCommandHandler(IMapper mapper, ITaskFileWriteRepository taskFileWriteRepository,
                                         TaskFileBusinessRules taskFileBusinessRules, ITaskFileReadRepository taskFileReadRepository, ITaskReadRepository taskReadRepository, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _taskFileWriteRepository = taskFileWriteRepository;
            _taskFileBusinessRules = taskFileBusinessRules;
            _taskFileReadRepository = taskFileReadRepository;
            _taskReadRepository = taskReadRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<CreatedTaskFileResponse> Handle(CreateTaskFileCommand request, CancellationToken cancellationToken)
        {
            await _taskFileBusinessRules.UserShouldExistWhenSelected(request.GidFileUploadUserFK);
            await _taskFileBusinessRules.TaskShouldExistWhenSelected(request.GidTaskFK);

            X.TaskFile taskFile = _mapper.Map<X.TaskFile>(request);


            await _taskFileWriteRepository.AddAsync(taskFile);
            await _taskFileWriteRepository.SaveAsync();

            X.TaskFile savedTaskFile = await _taskFileReadRepository.GetAsync(predicate: x => x.Gid == taskFile.Gid,
                include: i => i.Include(i => i.TaskFK).Include(i => i.UserFK));

            GetByGidTaskFileResponse obj = _mapper.Map<GetByGidTaskFileResponse>(savedTaskFile);
            return new()
            {
                Title = TaskFilesBusinessMessages.ProcessCompleted,
                Message = TaskFilesBusinessMessages.SuccessCreatedTaskFileMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}