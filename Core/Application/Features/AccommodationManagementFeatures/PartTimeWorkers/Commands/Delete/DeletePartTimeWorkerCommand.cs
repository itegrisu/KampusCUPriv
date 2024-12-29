using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Rules;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Delete;

public class DeletePartTimeWorkerCommand : IRequest<DeletedPartTimeWorkerResponse>
{
	public Guid Gid { get; set; }

    public class DeletePartTimeWorkerCommandHandler : IRequestHandler<DeletePartTimeWorkerCommand, DeletedPartTimeWorkerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
        private readonly IPartTimeWorkerWriteRepository _partTimeWorkerWriteRepository;
        private readonly PartTimeWorkerBusinessRules _partTimeWorkerBusinessRules;

        public DeletePartTimeWorkerCommandHandler(IMapper mapper, IPartTimeWorkerReadRepository partTimeWorkerReadRepository,
                                         PartTimeWorkerBusinessRules partTimeWorkerBusinessRules, IPartTimeWorkerWriteRepository partTimeWorkerWriteRepository)
        {
            _mapper = mapper;
            _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
            _partTimeWorkerBusinessRules = partTimeWorkerBusinessRules;
            _partTimeWorkerWriteRepository = partTimeWorkerWriteRepository;
        }

        public async Task<DeletedPartTimeWorkerResponse> Handle(DeletePartTimeWorkerCommand request, CancellationToken cancellationToken)
        {
            X.PartTimeWorker? partTimeWorker = await _partTimeWorkerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _partTimeWorkerBusinessRules.PartTimeWorkerShouldExistWhenSelected(partTimeWorker);
            partTimeWorker.DataState = Core.Enum.DataState.Deleted;

            _partTimeWorkerWriteRepository.Update(partTimeWorker);
            await _partTimeWorkerWriteRepository.SaveAsync();

            return new()
            {
                Title = PartTimeWorkersBusinessMessages.ProcessCompleted,
                Message = PartTimeWorkersBusinessMessages.SuccessDeletedPartTimeWorkerMessage,
                IsValid = true
            };
        }
    }
}