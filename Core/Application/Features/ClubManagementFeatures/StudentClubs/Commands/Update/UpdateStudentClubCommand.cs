using Application.Features.ClubFeatures.StudentClubs.Constants;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid;
using Application.Features.ClubFeatures.StudentClubs.Rules;
using AutoMapper;
using X = Domain.Entities.ClubManagements;
using MediatR;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Update;

public class UpdateStudentClubCommand : IRequest<UpdatedStudentClubResponse>
{
    public Guid Gid { get; set; }

	public Guid GidUserFK { get; set; }
public Guid GidClubFK { get; set; }




    public class UpdateStudentClubCommandHandler : IRequestHandler<UpdateStudentClubCommand, UpdatedStudentClubResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClubWriteRepository _studentClubWriteRepository;
        private readonly IStudentClubReadRepository _studentClubReadRepository;
        private readonly StudentClubBusinessRules _studentClubBusinessRules;

        public UpdateStudentClubCommandHandler(IMapper mapper, IStudentClubWriteRepository studentClubWriteRepository,
                                         StudentClubBusinessRules studentClubBusinessRules, IStudentClubReadRepository studentClubReadRepository)
        {
            _mapper = mapper;
            _studentClubWriteRepository = studentClubWriteRepository;
            _studentClubBusinessRules = studentClubBusinessRules;
            _studentClubReadRepository = studentClubReadRepository;
        }

        public async Task<UpdatedStudentClubResponse> Handle(UpdateStudentClubCommand request, CancellationToken cancellationToken)
        {
            X.StudentClub? studentClub = await _studentClubReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.ClubFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _studentClubBusinessRules.StudentClubShouldExistWhenSelected(studentClub);
            studentClub = _mapper.Map(request, studentClub);

            _studentClubWriteRepository.Update(studentClub!);
            await _studentClubWriteRepository.SaveAsync();
            GetByGidStudentClubResponse obj = _mapper.Map<GetByGidStudentClubResponse>(studentClub);

            return new()
            {
                Title = StudentClubsBusinessMessages.ProcessCompleted,
                Message = StudentClubsBusinessMessages.SuccessCreatedStudentClubMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}