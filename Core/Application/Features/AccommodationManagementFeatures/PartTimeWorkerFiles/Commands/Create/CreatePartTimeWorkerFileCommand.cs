using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Rules;
using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Create;

public class CreatePartTimeWorkerFileCommand : IRequest<CreatedPartTimeWorkerFileResponse>
{
    public Guid GidPartTimeWorkerFK { get; set; }

    public string Title { get; set; }
    public string? WorkerFile { get; set; }
    public DateTime? ExpiredDate { get; set; }



    public class CreatePartTimeWorkerFileCommandHandler : IRequestHandler<CreatePartTimeWorkerFileCommand, CreatedPartTimeWorkerFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerFileWriteRepository _partTimeWorkerFileWriteRepository;
        private readonly IPartTimeWorkerFileReadRepository _partTimeWorkerFileReadRepository;
        private readonly PartTimeWorkerFileBusinessRules _partTimeWorkerFileBusinessRules;

        public CreatePartTimeWorkerFileCommandHandler(IMapper mapper, IPartTimeWorkerFileWriteRepository partTimeWorkerFileWriteRepository,
                                         PartTimeWorkerFileBusinessRules partTimeWorkerFileBusinessRules, IPartTimeWorkerFileReadRepository partTimeWorkerFileReadRepository)
        {
            _mapper = mapper;
            _partTimeWorkerFileWriteRepository = partTimeWorkerFileWriteRepository;
            _partTimeWorkerFileBusinessRules = partTimeWorkerFileBusinessRules;
            _partTimeWorkerFileReadRepository = partTimeWorkerFileReadRepository;
        }

        public async Task<CreatedPartTimeWorkerFileResponse> Handle(CreatePartTimeWorkerFileCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _partTimeWorkerFileReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.PartTimeWorkerFile partTimeWorkerFile = _mapper.Map<X.PartTimeWorkerFile>(request);
            //partTimeWorkerFile.RowNo = maxRowNo + 1;

            await _partTimeWorkerFileWriteRepository.AddAsync(partTimeWorkerFile);
            await _partTimeWorkerFileWriteRepository.SaveAsync();

            X.PartTimeWorkerFile savedPartTimeWorkerFile = await _partTimeWorkerFileReadRepository.GetAsync(predicate: x => x.Gid == partTimeWorkerFile.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidPartTimeWorkerFileResponse obj = _mapper.Map<GetByGidPartTimeWorkerFileResponse>(savedPartTimeWorkerFile);
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