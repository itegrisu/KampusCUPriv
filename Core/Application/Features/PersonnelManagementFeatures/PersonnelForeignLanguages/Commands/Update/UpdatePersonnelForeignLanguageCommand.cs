using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Update;

public class UpdatePersonnelForeignLanguageCommand : IRequest<UpdatedPersonnelForeignLanguageResponse>
{
    public Guid Gid { get; set; }

    public Guid GidPersonnelFK { get; set; }
    public Guid GidLanguageFK { get; set; }

    public EnumLanguageLevel SpeakingLevel { get; set; }
    public EnumLanguageLevel ReadLevel { get; set; }




    public class UpdatePersonnelForeignLanguageCommandHandler : IRequestHandler<UpdatePersonnelForeignLanguageCommand, UpdatedPersonnelForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelForeignLanguageWriteRepository _personnelForeignLanguageWriteRepository;
        private readonly IPersonnelForeignLanguageReadRepository _personnelForeignLanguageReadRepository;
        private readonly PersonnelForeignLanguageBusinessRules _personnelForeignLanguageBusinessRules;

        public UpdatePersonnelForeignLanguageCommandHandler(IMapper mapper, IPersonnelForeignLanguageWriteRepository personnelForeignLanguageWriteRepository,
                                         PersonnelForeignLanguageBusinessRules personnelForeignLanguageBusinessRules, IPersonnelForeignLanguageReadRepository personnelForeignLanguageReadRepository)
        {
            _mapper = mapper;
            _personnelForeignLanguageWriteRepository = personnelForeignLanguageWriteRepository;
            _personnelForeignLanguageBusinessRules = personnelForeignLanguageBusinessRules;
            _personnelForeignLanguageReadRepository = personnelForeignLanguageReadRepository;
        }

        public async Task<UpdatedPersonnelForeignLanguageResponse> Handle(UpdatePersonnelForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelForeignLanguage? personnelForeignLanguage = await _personnelForeignLanguageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _personnelForeignLanguageBusinessRules.PersonnelForeignLanguageShouldExistWhenSelected(personnelForeignLanguage);
            await _personnelForeignLanguageBusinessRules.PersonnelShouldExistWhenSelected(request.GidPersonnelFK);
            await _personnelForeignLanguageBusinessRules.LanguageShouldExistWhenSelected(request.GidLanguageFK);
            personnelForeignLanguage = _mapper.Map(request, personnelForeignLanguage);

            _personnelForeignLanguageWriteRepository.Update(personnelForeignLanguage!);
            await _personnelForeignLanguageWriteRepository.SaveAsync();

            X.PersonnelForeignLanguage updatedPersonnelForeignLanguage = await _personnelForeignLanguageReadRepository.GetAsync(predicate: x => x.Gid == personnelForeignLanguage.Gid, include: x => x.Include(x => x.UserFK).Include(x => x.ForeignLanguageFK));

            GetByGidPersonnelForeignLanguageResponse obj = _mapper.Map<GetByGidPersonnelForeignLanguageResponse>(updatedPersonnelForeignLanguage);

            return new()
            {
                Title = PersonnelForeignLanguagesBusinessMessages.ProcessCompleted,
                Message = PersonnelForeignLanguagesBusinessMessages.SuccessCreatedPersonnelForeignLanguageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}