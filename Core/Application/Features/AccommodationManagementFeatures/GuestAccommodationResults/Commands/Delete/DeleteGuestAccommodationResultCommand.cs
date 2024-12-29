using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Delete;

public class DeleteGuestAccommodationResultCommand : IRequest<DeletedGuestAccommodationResultResponse>
{
	public Guid Gid { get; set; }

    public class DeleteGuestAccommodationResultCommandHandler : IRequestHandler<DeleteGuestAccommodationResultCommand, DeletedGuestAccommodationResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
        private readonly IGuestAccommodationResultWriteRepository _guestAccommodationResultWriteRepository;
        private readonly GuestAccommodationResultBusinessRules _guestAccommodationResultBusinessRules;

        public DeleteGuestAccommodationResultCommandHandler(IMapper mapper, IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository,
                                         GuestAccommodationResultBusinessRules guestAccommodationResultBusinessRules, IGuestAccommodationResultWriteRepository guestAccommodationResultWriteRepository)
        {
            _mapper = mapper;
            _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
            _guestAccommodationResultBusinessRules = guestAccommodationResultBusinessRules;
            _guestAccommodationResultWriteRepository = guestAccommodationResultWriteRepository;
        }

        public async Task<DeletedGuestAccommodationResultResponse> Handle(DeleteGuestAccommodationResultCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodationResult? guestAccommodationResult = await _guestAccommodationResultReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _guestAccommodationResultBusinessRules.GuestAccommodationResultShouldExistWhenSelected(guestAccommodationResult);
            guestAccommodationResult.DataState = Core.Enum.DataState.Deleted;

            _guestAccommodationResultWriteRepository.Update(guestAccommodationResult);
            await _guestAccommodationResultWriteRepository.SaveAsync();

            return new()
            {
                Title = GuestAccommodationResultsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationResultsBusinessMessages.SuccessDeletedGuestAccommodationResultMessage,
                IsValid = true
            };
        }
    }
}