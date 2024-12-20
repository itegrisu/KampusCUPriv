using Application.Features.AccommodationManagementFeatures.Guests.Constants;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Guests.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Domain.Enums;
using Application.Repositories.AccommodationManagements.GuestRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.Guests.Commands.Update;

public class UpdateGuestCommand : IRequest<UpdatedGuestResponse>
{
    public Guid Gid { get; set; }
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

    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, UpdatedGuestResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestWriteRepository _guestWriteRepository;
        private readonly IGuestReadRepository _guestReadRepository;
        private readonly GuestBusinessRules _guestBusinessRules;

        public UpdateGuestCommandHandler(IMapper mapper, IGuestWriteRepository guestWriteRepository,
                                         GuestBusinessRules guestBusinessRules, IGuestReadRepository guestReadRepository)
        {
            _mapper = mapper;
            _guestWriteRepository = guestWriteRepository;
            _guestBusinessRules = guestBusinessRules;
            _guestReadRepository = guestReadRepository;
        }

        public async Task<UpdatedGuestResponse> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            X.Guest? guest = await _guestReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.CountryFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _guestBusinessRules.GuestShouldExistWhenSelected(guest);
            guest = _mapper.Map(request, guest);

            _guestWriteRepository.Update(guest!);
            await _guestWriteRepository.SaveAsync();
            GetByGidGuestResponse obj = _mapper.Map<GetByGidGuestResponse>(guest);

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