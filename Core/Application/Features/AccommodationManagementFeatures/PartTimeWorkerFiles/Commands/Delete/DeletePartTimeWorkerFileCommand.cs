using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Rules;
using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Delete;

public class DeletePartTimeWorkerFileCommand : IRequest<DeletedPartTimeWorkerFileResponse>
{
	public Guid Gid { get; set; }

    public class DeletePartTimeWorkerFileCommandHandler : IRequestHandler<DeletePartTimeWorkerFileCommand, DeletedPartTimeWorkerFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerFileReadRepository _partTimeWorkerFileReadRepository;
        private readonly IPartTimeWorkerFileWriteRepository _partTimeWorkerFileWriteRepository;
        private readonly PartTimeWorkerFileBusinessRules _partTimeWorkerFileBusinessRules;

        public DeletePartTimeWorkerFileCommandHandler(IMapper mapper, IPartTimeWorkerFileReadRepository partTimeWorkerFileReadRepository,
                                         PartTimeWorkerFileBusinessRules partTimeWorkerFileBusinessRules, IPartTimeWorkerFileWriteRepository partTimeWorkerFileWriteRepository)
        {
            _mapper = mapper;
            _partTimeWorkerFileReadRepository = partTimeWorkerFileReadRepository;
            _partTimeWorkerFileBusinessRules = partTimeWorkerFileBusinessRules;
            _partTimeWorkerFileWriteRepository = partTimeWorkerFileWriteRepository;
        }

        public async Task<DeletedPartTimeWorkerFileResponse> Handle(DeletePartTimeWorkerFileCommand request, CancellationToken cancellationToken)
        {
            X.PartTimeWorkerFile? partTimeWorkerFile = await _partTimeWorkerFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _partTimeWorkerFileBusinessRules.PartTimeWorkerFileShouldExistWhenSelected(partTimeWorkerFile);
            partTimeWorkerFile.DataState = Core.Enum.DataState.Deleted;

            _partTimeWorkerFileWriteRepository.Update(partTimeWorkerFile);
            await _partTimeWorkerFileWriteRepository.SaveAsync();

            return new()
            {
                Title = PartTimeWorkerFilesBusinessMessages.ProcessCompleted,
                Message = PartTimeWorkerFilesBusinessMessages.SuccessDeletedPartTimeWorkerFileMessage,
                IsValid = true
            };
        }
    }
}