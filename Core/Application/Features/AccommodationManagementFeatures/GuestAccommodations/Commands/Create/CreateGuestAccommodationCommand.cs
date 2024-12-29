using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Create;

public class CreateGuestAccommodationCommand : IRequest<CreatedGuestAccommodationResponse>
{
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


    public class CreateGuestAccommodationCommandHandler : IRequestHandler<CreateGuestAccommodationCommand, CreatedGuestAccommodationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationWriteRepository _guestAccommodationWriteRepository;
        private readonly IGuestAccommodationReadRepository _guestAccommodationReadRepository;
        private readonly GuestAccommodationBusinessRules _guestAccommodationBusinessRules;

        public CreateGuestAccommodationCommandHandler(IMapper mapper, IGuestAccommodationWriteRepository guestAccommodationWriteRepository,
                                         GuestAccommodationBusinessRules guestAccommodationBusinessRules, IGuestAccommodationReadRepository guestAccommodationReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationWriteRepository = guestAccommodationWriteRepository;
            _guestAccommodationBusinessRules = guestAccommodationBusinessRules;
            _guestAccommodationReadRepository = guestAccommodationReadRepository;
        }

        public async Task<CreatedGuestAccommodationResponse> Handle(CreateGuestAccommodationCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _guestAccommodationReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.GuestAccommodation guestAccommodation = _mapper.Map<X.GuestAccommodation>(request);
            //guestAccommodation.RowNo = maxRowNo + 1;

            await _guestAccommodationWriteRepository.AddAsync(guestAccommodation);
            await _guestAccommodationWriteRepository.SaveAsync();

            X.GuestAccommodation savedGuestAccommodation = await _guestAccommodationReadRepository.GetAsync(predicate: x => x.Gid == guestAccommodation.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.BuyCurrencyFK).Include(x => x.SellCurrencyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidGuestAccommodationResponse obj = _mapper.Map<GetByGidGuestAccommodationResponse>(savedGuestAccommodation);
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