using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Update;

public class UpdateGuestAccommodationResultCommand : IRequest<UpdatedGuestAccommodationResultResponse>
{
    public Guid Gid { get; set; }
    public Guid GidGuestAccommodationPersonFK { get; set; }
    public Guid GidGuestAccommodationRoomFK { get; set; }
    public string? Note { get; set; }

    public class UpdateGuestAccommodationResultCommandHandler : IRequestHandler<UpdateGuestAccommodationResultCommand, UpdatedGuestAccommodationResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationResultWriteRepository _guestAccommodationResultWriteRepository;
        private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
        private readonly GuestAccommodationResultBusinessRules _guestAccommodationResultBusinessRules;

        public UpdateGuestAccommodationResultCommandHandler(IMapper mapper, IGuestAccommodationResultWriteRepository guestAccommodationResultWriteRepository,
                                         GuestAccommodationResultBusinessRules guestAccommodationResultBusinessRules, IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationResultWriteRepository = guestAccommodationResultWriteRepository;
            _guestAccommodationResultBusinessRules = guestAccommodationResultBusinessRules;
            _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
        }

        public async Task<UpdatedGuestAccommodationResultResponse> Handle(UpdateGuestAccommodationResultCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodationResult? guestAccommodationResult = await _guestAccommodationResultReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestAccommodationPersonFK).Include(x => x.GuestAccommodationRoomFK).ThenInclude(x => x.RoomTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _guestAccommodationResultBusinessRules.GuestAccommodationResultShouldExistWhenSelected(guestAccommodationResult);
            guestAccommodationResult = _mapper.Map(request, guestAccommodationResult);

            _guestAccommodationResultWriteRepository.Update(guestAccommodationResult!);
            await _guestAccommodationResultWriteRepository.SaveAsync();
            GetByGidGuestAccommodationResultResponse obj = _mapper.Map<GetByGidGuestAccommodationResultResponse>(guestAccommodationResult);

            return new()
            {
                Title = GuestAccommodationResultsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationResultsBusinessMessages.SuccessCreatedGuestAccommodationResultMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}