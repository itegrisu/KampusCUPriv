using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Create;

public class CreateGuestAccommodationResultCommand : IRequest<CreatedGuestAccommodationResultResponse>
{
    public Guid GidGuestAccommodationPersonFK { get; set; }
    public Guid GidGuestAccommodationRoomFK { get; set; }
    public string? Note { get; set; }

    public class CreateGuestAccommodationResultCommandHandler : IRequestHandler<CreateGuestAccommodationResultCommand, CreatedGuestAccommodationResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationResultWriteRepository _guestAccommodationResultWriteRepository;
        private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
        private readonly GuestAccommodationResultBusinessRules _guestAccommodationResultBusinessRules;

        public CreateGuestAccommodationResultCommandHandler(IMapper mapper, IGuestAccommodationResultWriteRepository guestAccommodationResultWriteRepository,
                                         GuestAccommodationResultBusinessRules guestAccommodationResultBusinessRules, IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationResultWriteRepository = guestAccommodationResultWriteRepository;
            _guestAccommodationResultBusinessRules = guestAccommodationResultBusinessRules;
            _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
        }

        public async Task<CreatedGuestAccommodationResultResponse> Handle(CreateGuestAccommodationResultCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _guestAccommodationResultReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.GuestAccommodationResult guestAccommodationResult = _mapper.Map<X.GuestAccommodationResult>(request);
            //guestAccommodationResult.RowNo = maxRowNo + 1;

            await _guestAccommodationResultWriteRepository.AddAsync(guestAccommodationResult);
            await _guestAccommodationResultWriteRepository.SaveAsync();

            X.GuestAccommodationResult savedGuestAccommodationResult = await _guestAccommodationResultReadRepository.GetAsync(predicate: x => x.Gid == guestAccommodationResult.Gid, include: x => x.Include(x => x.GuestAccommodationPersonFK).Include(x => x.GuestAccommodationRoomFK).ThenInclude(x => x.RoomTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidGuestAccommodationResultResponse obj = _mapper.Map<GetByGidGuestAccommodationResultResponse>(savedGuestAccommodationResult);
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