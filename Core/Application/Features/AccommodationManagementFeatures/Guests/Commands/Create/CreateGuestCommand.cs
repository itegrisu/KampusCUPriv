using Application.Features.AccommodationManagementFeatures.Guests.Constants;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Guests.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.GuestRepo;
using Domain.Enums;

namespace Application.Features.AccommodationManagementFeatures.Guests.Commands.Create;

public class CreateGuestCommand : IRequest<CreatedGuestResponse>
{
    public Guid GidNationalityFK { get; set; }
    public string IdNumber { get; set; }
    public string Name { get; set; }
    public string Surename { get; set; }
    public string? Duty { get; set; }
    public string? Institution { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public EnumGender Gender { get; set; }
    public string? HesCode { get; set; }
    public string? Description { get; set; }

    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, CreatedGuestResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestWriteRepository _guestWriteRepository;
        private readonly IGuestReadRepository _guestReadRepository;
        private readonly GuestBusinessRules _guestBusinessRules;

        public CreateGuestCommandHandler(IMapper mapper, IGuestWriteRepository guestWriteRepository,
                                         GuestBusinessRules guestBusinessRules, IGuestReadRepository guestReadRepository)
        {
            _mapper = mapper;
            _guestWriteRepository = guestWriteRepository;
            _guestBusinessRules = guestBusinessRules;
            _guestReadRepository = guestReadRepository;
        }

        public async Task<CreatedGuestResponse> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _guestReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Guest guest = _mapper.Map<X.Guest>(request);
            //guest.RowNo = maxRowNo + 1;

            await _guestWriteRepository.AddAsync(guest);
            await _guestWriteRepository.SaveAsync();

            X.Guest savedGuest = await _guestReadRepository.GetAsync(predicate: x => x.Gid == guest.Gid, include: x =>x.Include(x => x.CountryFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidGuestResponse obj = _mapper.Map<GetByGidGuestResponse>(savedGuest);
            return new()
            {
                Title = GuestsBusinessMessages.ProcessCompleted,
                Message = GuestsBusinessMessages.SuccessCreatedGuestMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}