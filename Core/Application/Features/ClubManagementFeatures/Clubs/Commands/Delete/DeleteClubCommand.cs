using Application.Features.ClubFeatures.Clubs.Constants;
using Application.Features.ClubFeatures.Clubs.Rules;
using AutoMapper;
using X = Domain.Entities.ClubManagements;
using MediatR;
using Application.Repositories.ClubManagementRepos.ClubRepo;

namespace Application.Features.ClubFeatures.Clubs.Commands.Delete;

public class DeleteClubCommand : IRequest<DeletedClubResponse>
{
	public Guid Gid { get; set; }

    public class DeleteClubCommandHandler : IRequestHandler<DeleteClubCommand, DeletedClubResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClubReadRepository _clubReadRepository;
        private readonly IClubWriteRepository _clubWriteRepository;
        private readonly ClubBusinessRules _clubBusinessRules;

        public DeleteClubCommandHandler(IMapper mapper, IClubReadRepository clubReadRepository,
                                         ClubBusinessRules clubBusinessRules, IClubWriteRepository clubWriteRepository)
        {
            _mapper = mapper;
            _clubReadRepository = clubReadRepository;
            _clubBusinessRules = clubBusinessRules;
            _clubWriteRepository = clubWriteRepository;
        }

        public async Task<DeletedClubResponse> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
        {
            X.Club? club = await _clubReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _clubBusinessRules.ClubShouldExistWhenSelected(club);
            club.DataState = Core.Enum.DataState.Deleted;

            _clubWriteRepository.Update(club);
            await _clubWriteRepository.SaveAsync();

            return new()
            {
                Title = ClubsBusinessMessages.ProcessCompleted,
                Message = ClubsBusinessMessages.SuccessDeletedClubMessage,
                IsValid = true
            };
        }
    }
}