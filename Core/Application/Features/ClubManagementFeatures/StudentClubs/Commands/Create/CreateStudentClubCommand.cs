using Application.Features.ClubFeatures.StudentClubs.Constants;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid;
using AutoMapper;
using X = Domain.Entities.ClubManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Application.Features.ClubFeatures.StudentClubs.Rules;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Create;

public class CreateStudentClubCommand : IRequest<CreatedStudentClubResponse>
{
    public Guid GidUserFK { get; set; }
    public Guid GidClubFK { get; set; }


    public class CreateStudentClubCommandHandler : IRequestHandler<CreateStudentClubCommand, CreatedStudentClubResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClubWriteRepository _studentClubWriteRepository;
        private readonly IStudentClubReadRepository _studentClubReadRepository;
        private readonly StudentClubBusinessRules _studentClubBusinessRules;

        public CreateStudentClubCommandHandler(IMapper mapper, IStudentClubWriteRepository studentClubWriteRepository,
                                         StudentClubBusinessRules studentClubBusinessRules, IStudentClubReadRepository studentClubReadRepository)
        {
            _mapper = mapper;
            _studentClubWriteRepository = studentClubWriteRepository;
            _studentClubBusinessRules = studentClubBusinessRules;
            _studentClubReadRepository = studentClubReadRepository;
        }

        public async Task<CreatedStudentClubResponse> Handle(CreateStudentClubCommand request, CancellationToken cancellationToken)
        {
            await _studentClubBusinessRules.UserCannotJoinSameClubTwice(request.GidUserFK, request.GidClubFK);
            //int maxRowNo = await _studentClubReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.StudentClub studentClub = _mapper.Map<X.StudentClub>(request);
            //studentClub.RowNo = maxRowNo + 1;

            await _studentClubWriteRepository.AddAsync(studentClub);
            await _studentClubWriteRepository.SaveAsync();

            X.StudentClub savedStudentClub = await _studentClubReadRepository.GetAsync(predicate: x => x.Gid == studentClub.Gid, include: x => x.Include(x => x.UserFK).Include(x => x.ClubFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidStudentClubResponse obj = _mapper.Map<GetByGidStudentClubResponse>(savedStudentClub);
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