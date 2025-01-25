using Application.Features.ClubFeatures.Clubs.Constants;
using Application.Features.ClubFeatures.Clubs.Queries.GetByGid;
using Application.Features.ClubFeatures.Clubs.Rules;
using AutoMapper;
using X = Domain.Entities.ClubManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.ClubManagementRepos.ClubRepo;

namespace Application.Features.ClubFeatures.Clubs.Commands.Create;

public class CreateClubCommand : IRequest<CreatedClubResponse>
{
    public Guid GidManagerFK { get; set; }
    public Guid GidCategoryFK { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }

    public class CreateClubCommandHandler : IRequestHandler<CreateClubCommand, CreatedClubResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClubWriteRepository _clubWriteRepository;
        private readonly IClubReadRepository _clubReadRepository;
        private readonly ClubBusinessRules _clubBusinessRules;

        public CreateClubCommandHandler(IMapper mapper, IClubWriteRepository clubWriteRepository,
                                         ClubBusinessRules clubBusinessRules, IClubReadRepository clubReadRepository)
        {
            _mapper = mapper;
            _clubWriteRepository = clubWriteRepository;
            _clubBusinessRules = clubBusinessRules;
            _clubReadRepository = clubReadRepository;
        }

        public async Task<CreatedClubResponse> Handle(CreateClubCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _clubReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Club club = _mapper.Map<X.Club>(request);
            //club.RowNo = maxRowNo + 1;

            await _clubWriteRepository.AddAsync(club);
            await _clubWriteRepository.SaveAsync();

            X.Club savedClub = await _clubReadRepository.GetAsync(predicate: x => x.Gid == club.Gid, include: x => x.Include(x => x.UserFK).Include(x=> x.CategoryFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidClubResponse obj = _mapper.Map<GetByGidClubResponse>(savedClub);
            return new()
            {
                Title = ClubsBusinessMessages.ProcessCompleted,
                Message = ClubsBusinessMessages.SuccessCreatedClubMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}