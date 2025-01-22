using Application.Features.ClubFeatures.StudentClubs.Constants;
using Application.Features.ClubFeatures.StudentClubs.Rules;
using AutoMapper;
using X = Domain.Entities.ClubManagements;
using MediatR;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Delete;

public class DeleteStudentClubCommand : IRequest<DeletedStudentClubResponse>
{
	public Guid Gid { get; set; }

    public class DeleteStudentClubCommandHandler : IRequestHandler<DeleteStudentClubCommand, DeletedStudentClubResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClubReadRepository _studentClubReadRepository;
        private readonly IStudentClubWriteRepository _studentClubWriteRepository;
        private readonly StudentClubBusinessRules _studentClubBusinessRules;

        public DeleteStudentClubCommandHandler(IMapper mapper, IStudentClubReadRepository studentClubReadRepository,
                                         StudentClubBusinessRules studentClubBusinessRules, IStudentClubWriteRepository studentClubWriteRepository)
        {
            _mapper = mapper;
            _studentClubReadRepository = studentClubReadRepository;
            _studentClubBusinessRules = studentClubBusinessRules;
            _studentClubWriteRepository = studentClubWriteRepository;
        }

        public async Task<DeletedStudentClubResponse> Handle(DeleteStudentClubCommand request, CancellationToken cancellationToken)
        {
            X.StudentClub? studentClub = await _studentClubReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _studentClubBusinessRules.StudentClubShouldExistWhenSelected(studentClub);
            studentClub.DataState = Core.Enum.DataState.Deleted;

            _studentClubWriteRepository.Update(studentClub);
            await _studentClubWriteRepository.SaveAsync();

            return new()
            {
                Title = StudentClubsBusinessMessages.ProcessCompleted,
                Message = StudentClubsBusinessMessages.SuccessDeletedStudentClubMessage,
                IsValid = true
            };
        }
    }
}