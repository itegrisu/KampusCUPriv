using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Delete;

public class DeletePersonnelDocumentCommand : IRequest<DeletedPersonnelDocumentResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelDocumentCommandHandler : IRequestHandler<DeletePersonnelDocumentCommand, DeletedPersonnelDocumentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelDocumentReadRepository _personnelDocumentReadRepository;
        private readonly IPersonnelDocumentWriteRepository _personnelDocumentWriteRepository;
        private readonly PersonnelDocumentBusinessRules _personnelDocumentBusinessRules;

        public DeletePersonnelDocumentCommandHandler(IMapper mapper, IPersonnelDocumentReadRepository personnelDocumentReadRepository,
                                         PersonnelDocumentBusinessRules personnelDocumentBusinessRules, IPersonnelDocumentWriteRepository personnelDocumentWriteRepository)
        {
            _mapper = mapper;
            _personnelDocumentReadRepository = personnelDocumentReadRepository;
            _personnelDocumentBusinessRules = personnelDocumentBusinessRules;
            _personnelDocumentWriteRepository = personnelDocumentWriteRepository;
        }

        public async Task<DeletedPersonnelDocumentResponse> Handle(DeletePersonnelDocumentCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelDocument? personnelDocument = await _personnelDocumentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelDocumentBusinessRules.PersonnelDocumentShouldExistWhenSelected(personnelDocument);
            personnelDocument.DataState = Core.Enum.DataState.Deleted;

            _personnelDocumentWriteRepository.Update(personnelDocument);
            await _personnelDocumentWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelDocumentsBusinessMessages.ProcessCompleted,
                Message = PersonnelDocumentsBusinessMessages.SuccessDeletedPersonnelDocumentMessage,
                IsValid = true
            };
        }
    }
}