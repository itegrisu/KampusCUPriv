using Application.Features.ClubFeatures.Clubs.Constants;
using Application.Features.ClubFeatures.Clubs.Queries.GetByGid;
using Application.Features.ClubFeatures.Clubs.Rules;
using AutoMapper;
using X = Domain.Entities.ClubManagements;
using MediatR;
using Application.Repositories.ClubManagementRepos.ClubRepo;
using Microsoft.EntityFrameworkCore;
using Application.Abstractions.Redis;

namespace Application.Features.ClubFeatures.Clubs.Commands.Update;

public class UpdateClubCommand : IRequest<UpdatedClubResponse>
{
    public Guid Gid { get; set; }
    public Guid GidManagerFK { get; set; }
    public Guid GidCategoryFK { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }

    public class UpdateClubCommandHandler : IRequestHandler<UpdateClubCommand, UpdatedClubResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClubWriteRepository _clubWriteRepository;
        private readonly IClubReadRepository _clubReadRepository;
        private readonly ClubBusinessRules _clubBusinessRules;
        private readonly IRedisCacheService _redisCacheService;
        public UpdateClubCommandHandler(IMapper mapper, IClubWriteRepository clubWriteRepository,
                                         ClubBusinessRules clubBusinessRules, IClubReadRepository clubReadRepository, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _clubWriteRepository = clubWriteRepository;
            _clubBusinessRules = clubBusinessRules;
            _clubReadRepository = clubReadRepository;
            _redisCacheService = redisCacheService;
        }

        public async Task<UpdatedClubResponse> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            X.Club? club = await _clubReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.CategoryFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _clubBusinessRules.ClubShouldExistWhenSelected(club);
            club = _mapper.Map(request, club);

            _clubWriteRepository.Update(club!);
            await _clubWriteRepository.SaveAsync();

            await _redisCacheService.RemoveByPattern("Clubs_");

            GetByGidClubResponse obj = _mapper.Map<GetByGidClubResponse>(club);

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