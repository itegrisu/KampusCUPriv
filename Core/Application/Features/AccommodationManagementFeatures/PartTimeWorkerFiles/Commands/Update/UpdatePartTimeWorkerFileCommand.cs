using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Rules;
using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Update;

public class UpdatePartTimeWorkerFileCommand : IRequest<UpdatedPartTimeWorkerFileResponse>
{
    public Guid Gid { get; set; }

	public Guid GidPartTimeWorkerFK { get; set; }

public string Title { get; set; }
public string? WorkerFile { get; set; }
public DateTime? ExpiredDate { get; set; }



    public class UpdatePartTimeWorkerFileCommandHandler : IRequestHandler<UpdatePartTimeWorkerFileCommand, UpdatedPartTimeWorkerFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerFileWriteRepository _partTimeWorkerFileWriteRepository;
        private readonly IPartTimeWorkerFileReadRepository _partTimeWorkerFileReadRepository;
        private readonly PartTimeWorkerFileBusinessRules _partTimeWorkerFileBusinessRules;

        public UpdatePartTimeWorkerFileCommandHandler(IMapper mapper, IPartTimeWorkerFileWriteRepository partTimeWorkerFileWriteRepository,
                                         PartTimeWorkerFileBusinessRules partTimeWorkerFileBusinessRules, IPartTimeWorkerFileReadRepository partTimeWorkerFileReadRepository)
        {
            _mapper = mapper;
            _partTimeWorkerFileWriteRepository = partTimeWorkerFileWriteRepository;
            _partTimeWorkerFileBusinessRules = partTimeWorkerFileBusinessRules;
            _partTimeWorkerFileReadRepository = partTimeWorkerFileReadRepository;
        }

        public async Task<UpdatedPartTimeWorkerFileResponse> Handle(UpdatePartTimeWorkerFileCommand request, CancellationToken cancellationToken)
        {
            X.PartTimeWorkerFile? partTimeWorkerFile = await _partTimeWorkerFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _partTimeWorkerFileBusinessRules.PartTimeWorkerFileShouldExistWhenSelected(partTimeWorkerFile);
            partTimeWorkerFile = _mapper.Map(request, partTimeWorkerFile);

            _partTimeWorkerFileWriteRepository.Update(partTimeWorkerFile!);
            await _partTimeWorkerFileWriteRepository.SaveAsync();
            GetByGidPartTimeWorkerFileResponse obj = _mapper.Map<GetByGidPartTimeWorkerFileResponse>(partTimeWorkerFile);

            return new()
            {
                Title = PartTimeWorkerFilesBusinessMessages.ProcessCompleted,
                Message = PartTimeWorkerFilesBusinessMessages.SuccessCreatedPartTimeWorkerFileMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}