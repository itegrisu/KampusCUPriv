using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Rules;
using Application.Helpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Create;

public class CreatePartTimeWorkerCommand : IRequest<CreatedPartTimeWorkerResponse>
{

    public string IdentityNo { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }
    public bool IsLoginStatus { get; set; }
    public string Gsm { get; set; }
    public DateTime BirthDate { get; set; }



    public class CreatePartTimeWorkerCommandHandler : IRequestHandler<CreatePartTimeWorkerCommand, CreatedPartTimeWorkerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerWriteRepository _partTimeWorkerWriteRepository;
        private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
        private readonly PartTimeWorkerBusinessRules _partTimeWorkerBusinessRules;

        public CreatePartTimeWorkerCommandHandler(IMapper mapper, IPartTimeWorkerWriteRepository partTimeWorkerWriteRepository,
                                         PartTimeWorkerBusinessRules partTimeWorkerBusinessRules, IPartTimeWorkerReadRepository partTimeWorkerReadRepository)
        {
            _mapper = mapper;
            _partTimeWorkerWriteRepository = partTimeWorkerWriteRepository;
            _partTimeWorkerBusinessRules = partTimeWorkerBusinessRules;
            _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
        }

        public async Task<CreatedPartTimeWorkerResponse> Handle(CreatePartTimeWorkerCommand request, CancellationToken cancellationToken)
        {
            await _partTimeWorkerBusinessRules.IsUserNameUnique(request.UserName);

            //int maxRowNo = await _partTimeWorkerReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.PartTimeWorker partTimeWorker = _mapper.Map<X.PartTimeWorker>(request);
            //partTimeWorker.RowNo = maxRowNo + 1;

            string passwordHash, passwordSalt;
            HashingHelperForApplicationLayer.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            partTimeWorker.Password = passwordHash;
            partTimeWorker.PasswordHash = passwordSalt;

            await _partTimeWorkerWriteRepository.AddAsync(partTimeWorker);
            await _partTimeWorkerWriteRepository.SaveAsync();

            X.PartTimeWorker savedPartTimeWorker = await _partTimeWorkerReadRepository.GetAsync(predicate: x => x.Gid == partTimeWorker.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidPartTimeWorkerResponse obj = _mapper.Map<GetByGidPartTimeWorkerResponse>(savedPartTimeWorker);
            return new()
            {
                Title = PartTimeWorkersBusinessMessages.ProcessCompleted,
                Message = PartTimeWorkersBusinessMessages.SuccessCreatedPartTimeWorkerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}