using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Create;

public class CreatePersonnelAddressCommand : IRequest<CreatedPersonnelAddressResponse>
{
    public Guid GidPersonelFK { get; set; }
    public Guid GidSehirFK { get; set; }
    public string AdresBasligi { get; set; }
    public string Adres { get; set; }
    public string? Aciklama { get; set; }



    public class CreatePersonnelAddressCommandHandler : IRequestHandler<CreatePersonnelAddressCommand, CreatedPersonnelAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelAddressWriteRepository _personnelAddressWriteRepository;
        private readonly IPersonnelAddressReadRepository _personnelAddressReadRepository;
        private readonly PersonnelAddressBusinessRules _personnelAddressBusinessRules;

        public CreatePersonnelAddressCommandHandler(IMapper mapper, IPersonnelAddressWriteRepository personnelAddressWriteRepository,
                                         PersonnelAddressBusinessRules personnelAddressBusinessRules, IPersonnelAddressReadRepository personnelAddressReadRepository)
        {
            _mapper = mapper;
            _personnelAddressWriteRepository = personnelAddressWriteRepository;
            _personnelAddressBusinessRules = personnelAddressBusinessRules;
            _personnelAddressReadRepository = personnelAddressReadRepository;
        }

        public async Task<CreatedPersonnelAddressResponse> Handle(CreatePersonnelAddressCommand request, CancellationToken cancellationToken)
        {
            await _personnelAddressBusinessRules.UserShouldExistWhenSelected(request.GidPersonelFK);
            await _personnelAddressBusinessRules.CityShouldExistWhenSelected(request.GidSehirFK);

            X.PersonnelAddress personnelAddress = _mapper.Map<X.PersonnelAddress>(request);

            await _personnelAddressWriteRepository.AddAsync(personnelAddress);
            await _personnelAddressWriteRepository.SaveAsync();

            X.PersonnelAddress savedPersonnelAddress = await _personnelAddressReadRepository.GetAsync(predicate: x => x.Gid == personnelAddress.Gid,
                include: x => x.Include(x => x.UserFK).Include(x => x.CityFK)
                );

            GetByGidPersonnelAddressResponse obj = _mapper.Map<GetByGidPersonnelAddressResponse>(savedPersonnelAddress);
            return new()
            {
                Title = PersonnelAddressesBusinessMessages.ProcessCompleted,
                Message = PersonnelAddressesBusinessMessages.SuccessCreatedPersonnelAddressMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}