using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Rules;
using Application.Helpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Update;

public class UpdatePartTimeWorkerCommand : IRequest<UpdatedPartTimeWorkerResponse>
{
    public Guid Gid { get; set; }


    public string IdentityNo { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string? Password { get; set; }
    public string? PasswordHash { get; set; }
    public bool IsLoginStatus { get; set; }
    public string Gsm { get; set; }
    public DateTime BirthDate { get; set; }



    public class UpdatePartTimeWorkerCommandHandler : IRequestHandler<UpdatePartTimeWorkerCommand, UpdatedPartTimeWorkerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerWriteRepository _partTimeWorkerWriteRepository;
        private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
        private readonly PartTimeWorkerBusinessRules _partTimeWorkerBusinessRules;

        public UpdatePartTimeWorkerCommandHandler(IMapper mapper, IPartTimeWorkerWriteRepository partTimeWorkerWriteRepository,
                                         PartTimeWorkerBusinessRules partTimeWorkerBusinessRules, IPartTimeWorkerReadRepository partTimeWorkerReadRepository)
        {
            _mapper = mapper;
            _partTimeWorkerWriteRepository = partTimeWorkerWriteRepository;
            _partTimeWorkerBusinessRules = partTimeWorkerBusinessRules;
            _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
        }

        public async Task<UpdatedPartTimeWorkerResponse> Handle(UpdatePartTimeWorkerCommand request, CancellationToken cancellationToken)
        {
            X.PartTimeWorker? partTimeWorker = await _partTimeWorkerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _partTimeWorkerBusinessRules.PartTimeWorkerShouldExistWhenSelected(partTimeWorker);
            if (request.Password != null)
            {
                string passwordHash, passwordSalt;
                HashingHelperForApplicationLayer.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                request.Password = passwordHash;
                request.PasswordHash = passwordSalt;
            }
            else
            {
                request.Password = partTimeWorker.Password;
                request.PasswordHash = partTimeWorker.PasswordHash;
            }
            partTimeWorker = _mapper.Map(request, partTimeWorker);

            if (request.UserName != partTimeWorker.UserName)
                await _partTimeWorkerBusinessRules.IsUserNameUnique(request.UserName);


            _partTimeWorkerWriteRepository.Update(partTimeWorker!);
            await _partTimeWorkerWriteRepository.SaveAsync();
            GetByGidPartTimeWorkerResponse obj = _mapper.Map<GetByGidPartTimeWorkerResponse>(partTimeWorker);

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