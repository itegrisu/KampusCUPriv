using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Update;

public class UpdateGuestAccommodationCommand : IRequest<UpdatedGuestAccommodationResponse>
{
    public Guid Gid { get; set; }
    public Guid GidHotelFK { get; set; }
    public Guid GidBuyCurrencyFK { get; set; }
    public Guid GidSellCurrencyFK { get; set; }
    public string Title { get; set; }
    public string? Institution { get; set; }
    public int GuestCount { get; set; }
    public string? Description { get; set; }
    public EnumGuestAccommodationStatus GuestAccommodationStatus { get; set; }
    public decimal? BuyPrice { get; set; }
    public decimal? SellPrice { get; set; }



    public class UpdateGuestAccommodationCommandHandler : IRequestHandler<UpdateGuestAccommodationCommand, UpdatedGuestAccommodationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationWriteRepository _guestAccommodationWriteRepository;
        private readonly IGuestAccommodationReadRepository _guestAccommodationReadRepository;
        private readonly GuestAccommodationBusinessRules _guestAccommodationBusinessRules;

        public UpdateGuestAccommodationCommandHandler(IMapper mapper, IGuestAccommodationWriteRepository guestAccommodationWriteRepository,
                                         GuestAccommodationBusinessRules guestAccommodationBusinessRules, IGuestAccommodationReadRepository guestAccommodationReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationWriteRepository = guestAccommodationWriteRepository;
            _guestAccommodationBusinessRules = guestAccommodationBusinessRules;
            _guestAccommodationReadRepository = guestAccommodationReadRepository;
        }

        public async Task<UpdatedGuestAccommodationResponse> Handle(UpdateGuestAccommodationCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodation? guestAccommodation = await _guestAccommodationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.BuyCurrencyFK).Include(x => x.SellCurrencyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _guestAccommodationBusinessRules.GuestAccommodationShouldExistWhenSelected(guestAccommodation);
            guestAccommodation = _mapper.Map(request, guestAccommodation);

            _guestAccommodationWriteRepository.Update(guestAccommodation!);
            await _guestAccommodationWriteRepository.SaveAsync();
            GetByGidGuestAccommodationResponse obj = _mapper.Map<GetByGidGuestAccommodationResponse>(guestAccommodation);

            return new()
            {
                Title = GuestAccommodationsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationsBusinessMessages.SuccessCreatedGuestAccommodationMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}