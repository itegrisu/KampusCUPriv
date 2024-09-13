using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Create;

public class CreatePersonnelForeignLanguageCommand : IRequest<CreatedPersonnelForeignLanguageResponse>
{
    public Guid GidPersonelFK { get; set; }
    public Guid GidLanguageFK { get; set; }
    public EnumKonusmaDuzeyi KonusmaDuzeyi { get; set; }
    public EnumOkumaDuzeyi OkumaDuzeyi { get; set; }



    public class CreatePersonnelForeignLanguageCommandHandler : IRequestHandler<CreatePersonnelForeignLanguageCommand, CreatedPersonnelForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelForeignLanguageWriteRepository _personnelForeignLanguageWriteRepository;
        private readonly IPersonnelForeignLanguageReadRepository _personnelForeignLanguageReadRepository;
        private readonly PersonnelForeignLanguageBusinessRules _personnelForeignLanguageBusinessRules;

        public CreatePersonnelForeignLanguageCommandHandler(IMapper mapper, IPersonnelForeignLanguageWriteRepository personnelForeignLanguageWriteRepository,
                                         PersonnelForeignLanguageBusinessRules personnelForeignLanguageBusinessRules, IPersonnelForeignLanguageReadRepository personnelForeignLanguageReadRepository)
        {
            _mapper = mapper;
            _personnelForeignLanguageWriteRepository = personnelForeignLanguageWriteRepository;
            _personnelForeignLanguageBusinessRules = personnelForeignLanguageBusinessRules;
            _personnelForeignLanguageReadRepository = personnelForeignLanguageReadRepository;
        }

        public async Task<CreatedPersonnelForeignLanguageResponse> Handle(CreatePersonnelForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            await _personnelForeignLanguageBusinessRules.PersonnelShouldExistWhenSelected(request.GidPersonelFK);
            await _personnelForeignLanguageBusinessRules.LanguageShouldExistWhenSelected(request.GidLanguageFK);

            X.PersonnelForeignLanguage personnelForeignLanguage = _mapper.Map<X.PersonnelForeignLanguage>(request);

            await _personnelForeignLanguageWriteRepository.AddAsync(personnelForeignLanguage);
            await _personnelForeignLanguageWriteRepository.SaveAsync();

            X.PersonnelForeignLanguage savedPersonnelForeignLanguage = await _personnelForeignLanguageReadRepository.GetAsync(predicate: x => x.Gid == personnelForeignLanguage.Gid, include: x => x.Include(x => x.UserFK).Include(x => x.ForeignLanguageFK));


            GetByGidPersonnelForeignLanguageResponse obj = _mapper.Map<GetByGidPersonnelForeignLanguageResponse>(savedPersonnelForeignLanguage);
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