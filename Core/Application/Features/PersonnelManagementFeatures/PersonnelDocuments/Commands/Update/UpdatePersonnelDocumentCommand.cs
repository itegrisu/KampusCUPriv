using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Update;

public class UpdatePersonnelDocumentCommand : IRequest<UpdatedPersonnelDocumentResponse>
{
    public Guid Gid { get; set; }

    public Guid GidPersonnelFK { get; set; }
    public Guid GidDocumentType { get; set; }

    public string Name { get; set; }
    public DateTime? ValidityDate { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class UpdatePersonnelDocumentCommandHandler : IRequestHandler<UpdatePersonnelDocumentCommand, UpdatedPersonnelDocumentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelDocumentWriteRepository _personnelDocumentWriteRepository;
        private readonly IPersonnelDocumentReadRepository _personnelDocumentReadRepository;
        private readonly PersonnelDocumentBusinessRules _personnelDocumentBusinessRules;

        public UpdatePersonnelDocumentCommandHandler(IMapper mapper, IPersonnelDocumentWriteRepository personnelDocumentWriteRepository,
                                         PersonnelDocumentBusinessRules personnelDocumentBusinessRules, IPersonnelDocumentReadRepository personnelDocumentReadRepository)
        {
            _mapper = mapper;
            _personnelDocumentWriteRepository = personnelDocumentWriteRepository;
            _personnelDocumentBusinessRules = personnelDocumentBusinessRules;
            _personnelDocumentReadRepository = personnelDocumentReadRepository;
        }

        public async Task<UpdatedPersonnelDocumentResponse> Handle(UpdatePersonnelDocumentCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelDocument? personnelDocument = await _personnelDocumentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _personnelDocumentBusinessRules.PersonnelDocumentShouldExistWhenSelected(personnelDocument);
            await _personnelDocumentBusinessRules.UserShouldExistWhenSelected(request.GidPersonnelFK);
            await _personnelDocumentBusinessRules.DocumentTypeShouldExistWhenSelected(request.GidDocumentType);
            personnelDocument = _mapper.Map(request, personnelDocument);

            _personnelDocumentWriteRepository.Update(personnelDocument!);
            await _personnelDocumentWriteRepository.SaveAsync();

            X.PersonnelDocument updatedPersonnelDocument = await _personnelDocumentReadRepository.GetAsync(predicate: x => x.Gid == personnelDocument.Gid,
                include: x => x.Include(x => x.UserFK).Include(x => x.DocumentTypeFK));

            GetByGidPersonnelDocumentResponse obj = _mapper.Map<GetByGidPersonnelDocumentResponse>(updatedPersonnelDocument);

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