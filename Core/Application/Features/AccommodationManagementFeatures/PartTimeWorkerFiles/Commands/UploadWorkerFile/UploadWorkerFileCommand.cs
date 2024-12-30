using Application.Abstractions.Storage;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.UploadDocumentFile;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using Domain.Entities.AccommodationManagements;
using MediatR;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.UploadWorkerFile
{
    public class UploadWorkerFileCommand : IRequest<UploadWorkerFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }

        public class UploadWorkerFileCommandHandler : IRequestHandler<UploadWorkerFileCommand, UploadWorkerFileResponse>
        {
            private readonly IStorageService _storageService;
            private readonly IPartTimeWorkerFileReadRepository _partTimeWorkerFileReadRepository;
            private readonly IPartTimeWorkerFileWriteRepository _partTimeWorkerFileWriteRepository;

            public UploadWorkerFileCommandHandler(IStorageService storageService, IPartTimeWorkerFileReadRepository partTimeWorkerFileReadRepository, IPartTimeWorkerFileWriteRepository partTimeWorkerFileWriteRepository)
            {
                _storageService = storageService;
                _partTimeWorkerFileReadRepository = partTimeWorkerFileReadRepository;
                _partTimeWorkerFileWriteRepository = partTimeWorkerFileWriteRepository;
            }

            public async Task<UploadWorkerFileResponse> Handle(UploadWorkerFileCommand request, CancellationToken cancellationToken)
            {

                PartTimeWorkerFile? file = await _partTimeWorkerFileReadRepository.GetSingleAsync(u => u.Gid == request.Gid);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/part-time-worker-file");

                file.WorkerFile = "\\Files\\part-time-worker-file\\" + request.FileName;

                _partTimeWorkerFileWriteRepository.Update(file);
                await _partTimeWorkerFileWriteRepository.SaveAsync();

                return new()
                {
                    FullPath = file.WorkerFile,
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = PartTimeWorkerFilesBusinessMessages.SuccessUpload,
                    IsValid = true
                };
            }
        }
    }
}