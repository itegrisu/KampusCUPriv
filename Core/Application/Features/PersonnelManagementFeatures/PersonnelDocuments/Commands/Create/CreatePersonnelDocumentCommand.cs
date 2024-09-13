using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Create;

public class CreatePersonnelDocumentCommand : IRequest<CreatedPersonnelDocumentResponse>
{
    public Guid GidPersonelFK { get; set; }
    public Guid GidBelgeTuru { get; set; }
    public string BelgeAdi { get; set; }
    public DateTime? GecerlilikTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }



    public class CreatePersonnelDocumentCommandHandler : IRequestHandler<CreatePersonnelDocumentCommand, CreatedPersonnelDocumentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelDocumentWriteRepository _personnelDocumentWriteRepository;
        private readonly IPersonnelDocumentReadRepository _personnelDocumentReadRepository;
        private readonly PersonnelDocumentBusinessRules _personnelDocumentBusinessRules;

        public CreatePersonnelDocumentCommandHandler(IMapper mapper, IPersonnelDocumentWriteRepository personnelDocumentWriteRepository,
                                         PersonnelDocumentBusinessRules personnelDocumentBusinessRules, IPersonnelDocumentReadRepository personnelDocumentReadRepository)
        {
            _mapper = mapper;
            _personnelDocumentWriteRepository = personnelDocumentWriteRepository;
            _personnelDocumentBusinessRules = personnelDocumentBusinessRules;
            _personnelDocumentReadRepository = personnelDocumentReadRepository;
        }

        public async Task<CreatedPersonnelDocumentResponse> Handle(CreatePersonnelDocumentCommand request, CancellationToken cancellationToken)
        {
            await _personnelDocumentBusinessRules.UserShouldExistWhenSelected(request.GidPersonelFK);
            await _personnelDocumentBusinessRules.DocumentTypeShouldExistWhenSelected(request.GidBelgeTuru);

            X.PersonnelDocument personnelDocument = _mapper.Map<X.PersonnelDocument>(request);

            await _personnelDocumentWriteRepository.AddAsync(personnelDocument);
            await _personnelDocumentWriteRepository.SaveAsync();

            X.PersonnelDocument savedPersonnelDocument = await _personnelDocumentReadRepository.GetAsync(predicate: x => x.Gid == personnelDocument.Gid,
                include: x => x.Include(x => x.UserFK).Include(x => x.DocumentTypeFK));


            GetByGidPersonnelDocumentResponse obj = _mapper.Map<GetByGidPersonnelDocumentResponse>(savedPersonnelDocument);
            return new()
            {
                Title = PersonnelDocumentsBusinessMessages.ProcessCompleted,
                Message = PersonnelDocumentsBusinessMessages.SuccessCreatedPersonnelDocumentMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}