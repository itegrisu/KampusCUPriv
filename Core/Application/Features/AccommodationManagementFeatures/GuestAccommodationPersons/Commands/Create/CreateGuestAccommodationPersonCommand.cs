using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Constants;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Create;

public class CreateGuestAccommodationPersonCommand : IRequest<CreatedGuestAccommodationPersonResponse>
{
    public Guid GidGuestAccommodationFK { get; set; }
    public Guid GidNationalityFK { get; set; }
    public string? IdNumber { get; set; }
    public string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Description { get; set; }

    public class CreateGuestAccommodationPersonCommandHandler : IRequestHandler<CreateGuestAccommodationPersonCommand, CreatedGuestAccommodationPersonResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGuestAccommodationPersonWriteRepository _guestAccommodationPersonWriteRepository;
        private readonly IGuestAccommodationPersonReadRepository _guestAccommodationPersonReadRepository;
        private readonly GuestAccommodationPersonBusinessRules _guestAccommodationPersonBusinessRules;

        public CreateGuestAccommodationPersonCommandHandler(IMapper mapper, IGuestAccommodationPersonWriteRepository guestAccommodationPersonWriteRepository,
                                         GuestAccommodationPersonBusinessRules guestAccommodationPersonBusinessRules, IGuestAccommodationPersonReadRepository guestAccommodationPersonReadRepository)
        {
            _mapper = mapper;
            _guestAccommodationPersonWriteRepository = guestAccommodationPersonWriteRepository;
            _guestAccommodationPersonBusinessRules = guestAccommodationPersonBusinessRules;
            _guestAccommodationPersonReadRepository = guestAccommodationPersonReadRepository;
        }

        public async Task<CreatedGuestAccommodationPersonResponse> Handle(CreateGuestAccommodationPersonCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _guestAccommodationPersonReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.GuestAccommodationPerson guestAccommodationPerson = _mapper.Map<X.GuestAccommodationPerson>(request);
            //guestAccommodationPerson.RowNo = maxRowNo + 1;

            await _guestAccommodationPersonWriteRepository.AddAsync(guestAccommodationPerson);
            await _guestAccommodationPersonWriteRepository.SaveAsync();

            X.GuestAccommodationPerson savedGuestAccommodationPerson = await _guestAccommodationPersonReadRepository.GetAsync(predicate: x => x.Gid == guestAccommodationPerson.Gid, include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.CountryFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidGuestAccommodationPersonResponse obj = _mapper.Map<GetByGidGuestAccommodationPersonResponse>(savedGuestAccommodationPerson);
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