using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Update;

public class UpdateGuestAccommodationPersonCommand : IRequest<UpdatedGuestAccommodationPersonResponse>
{
    public Guid Gid { get; set; }
    public Guid GidGuestAccommodationFK { get; set; }
    public Guid GidNationalityFK { get; set; }
    public string? IdNumber { get; set; }
    public string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Description { get; set; }

    public class UpdateGuestAccommodationPersonCommandHandler : IRequestHandler<UpdateGuestAccommodationPersonCommand, UpdatedGuestAccommodationPersonResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationPersonWriteRepository _guestAccommodationPersonWriteRepository;
        private readonly IGuestAccommodationPersonReadRepository _guestAccommodationPersonReadRepository;
        private readonly GuestAccommodationPersonBusinessRules _guestAccommodationPersonBusinessRules;

        public UpdateGuestAccommodationPersonCommandHandler(IMapper mapper, IGuestAccommodationPersonWriteRepository guestAccommodationPersonWriteRepository,
                                         GuestAccommodationPersonBusinessRules guestAccommodationPersonBusinessRules, IGuestAccommodationPersonReadRepository guestAccommodationPersonReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationPersonWriteRepository = guestAccommodationPersonWriteRepository;
            _guestAccommodationPersonBusinessRules = guestAccommodationPersonBusinessRules;
            _guestAccommodationPersonReadRepository = guestAccommodationPersonReadRepository;
        }

        public async Task<UpdatedGuestAccommodationPersonResponse> Handle(UpdateGuestAccommodationPersonCommand request, CancellationToken cancellationToken)
        {
            X.GuestAccommodationPerson? guestAccommodationPerson = await _guestAccommodationPersonReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.CountryFK).Include(x => x.GuestAccommodationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _guestAccommodationPersonBusinessRules.GuestAccommodationPersonShouldExistWhenSelected(guestAccommodationPerson);
            guestAccommodationPerson = _mapper.Map(request, guestAccommodationPerson);

            _guestAccommodationPersonWriteRepository.Update(guestAccommodationPerson!);
            await _guestAccommodationPersonWriteRepository.SaveAsync();

            X.GuestAccommodationPerson? guestAccommodationPersonUpdated = await _guestAccommodationPersonReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.CountryFK).Include(x => x.GuestAccommodationFK));

            GetByGidGuestAccommodationPersonResponse obj = _mapper.Map<GetByGidGuestAccommodationPersonResponse>(guestAccommodationPersonUpdated);

            return new()
            {
                Title = GuestAccommodationPersonsBusinessMessages.ProcessCompleted,
                Message = GuestAccommodationPersonsBusinessMessages.SuccessCreatedGuestAccommodationPersonMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}