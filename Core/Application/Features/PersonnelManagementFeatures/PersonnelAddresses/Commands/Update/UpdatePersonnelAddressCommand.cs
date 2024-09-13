using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Update;

public class UpdatePersonnelAddressCommand : IRequest<UpdatedPersonnelAddressResponse>
{
    public Guid Gid { get; set; }
    public Guid GidPersonelFK { get; set; }
    public Guid GidSehirFK { get; set; }
    public string AdresBasligi { get; set; }
    public string Adres { get; set; }
    public string? Aciklama { get; set; }



    public class UpdatePersonnelAddressCommandHandler : IRequestHandler<UpdatePersonnelAddressCommand, UpdatedPersonnelAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelAddressWriteRepository _personnelAddressWriteRepository;
        private readonly IPersonnelAddressReadRepository _personnelAddressReadRepository;
        private readonly PersonnelAddressBusinessRules _personnelAddressBusinessRules;

        public UpdatePersonnelAddressCommandHandler(IMapper mapper, IPersonnelAddressWriteRepository personnelAddressWriteRepository,
                                         PersonnelAddressBusinessRules personnelAddressBusinessRules, IPersonnelAddressReadRepository personnelAddressReadRepository)
        {
            _mapper = mapper;
            _personnelAddressWriteRepository = personnelAddressWriteRepository;
            _personnelAddressBusinessRules = personnelAddressBusinessRules;
            _personnelAddressReadRepository = personnelAddressReadRepository;
        }

        public async Task<UpdatedPersonnelAddressResponse> Handle(UpdatePersonnelAddressCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelAddress? personnelAddress = await _personnelAddressReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _personnelAddressBusinessRules.PersonnelAddressShouldExistWhenSelected(personnelAddress);
            await _personnelAddressBusinessRules.UserShouldExistWhenSelected(request.GidPersonelFK);
            await _personnelAddressBusinessRules.CityShouldExistWhenSelected(request.GidSehirFK);

            personnelAddress = _mapper.Map(request, personnelAddress);

            _personnelAddressWriteRepository.Update(personnelAddress!);
            await _personnelAddressWriteRepository.SaveAsync();

            X.PersonnelAddress updatedPersonnelAddress = await _personnelAddressReadRepository.GetAsync(predicate: x => x.Gid == personnelAddress.Gid,
                include: x => x.Include(x => x.UserFK).Include(x => x.CityFK));
            GetByGidPersonnelAddressResponse obj = _mapper.Map<GetByGidPersonnelAddressResponse>(updatedPersonnelAddress);

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