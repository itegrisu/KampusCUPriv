using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Update;

public class UpdatePersonnelPassportInfoCommand : IRequest<UpdatedPersonnelPassportInfoResponse>
{
    public Guid Gid { get; set; }

    public Guid GidPersonelFK { get; set; }

    public string PasaportNo { get; set; }
    public DateTime VerilisTarihi { get; set; }
    public DateTime GecerlilikTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }



    public class UpdatePersonnelPassportInfoCommandHandler : IRequestHandler<UpdatePersonnelPassportInfoCommand, UpdatedPersonnelPassportInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPassportInfoWriteRepository _personnelPassportInfoWriteRepository;
        private readonly IPersonnelPassportInfoReadRepository _personnelPassportInfoReadRepository;
        private readonly PersonnelPassportInfoBusinessRules _personnelPassportInfoBusinessRules;

        public UpdatePersonnelPassportInfoCommandHandler(IMapper mapper, IPersonnelPassportInfoWriteRepository personnelPassportInfoWriteRepository,
                                         PersonnelPassportInfoBusinessRules personnelPassportInfoBusinessRules, IPersonnelPassportInfoReadRepository personnelPassportInfoReadRepository)
        {
            _mapper = mapper;
            _personnelPassportInfoWriteRepository = personnelPassportInfoWriteRepository;
            _personnelPassportInfoBusinessRules = personnelPassportInfoBusinessRules;
            _personnelPassportInfoReadRepository = personnelPassportInfoReadRepository;
        }

        public async Task<UpdatedPersonnelPassportInfoResponse> Handle(UpdatePersonnelPassportInfoCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelPassportInfo? personnelPassportInfo = await _personnelPassportInfoReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _personnelPassportInfoBusinessRules.PersonnelPassportInfoShouldExistWhenSelected(personnelPassportInfo);
            await _personnelPassportInfoBusinessRules.UserShouldExistWhenSelected(request.GidPersonelFK);
            personnelPassportInfo = _mapper.Map(request, personnelPassportInfo);

            _personnelPassportInfoWriteRepository.Update(personnelPassportInfo!);
            await _personnelPassportInfoWriteRepository.SaveAsync();
            X.PersonnelPassportInfo updatedPersonnelPassportInfo = await _personnelPassportInfoReadRepository.GetAsync(predicate: x => x.Gid == personnelPassportInfo.Gid, include: x => x.Include(x => x.UserFK));

            GetByGidPersonnelPassportInfoResponse obj = _mapper.Map<GetByGidPersonnelPassportInfoResponse>(updatedPersonnelPassportInfo);

            return new()
            {
                Title = PersonnelPassportInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelPassportInfosBusinessMessages.SuccessCreatedPersonnelPassportInfoMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}