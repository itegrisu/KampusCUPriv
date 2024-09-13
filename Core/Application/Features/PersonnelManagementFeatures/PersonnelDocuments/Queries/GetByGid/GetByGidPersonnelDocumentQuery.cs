using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid
{
    public class GetByGidPersonnelDocumentQuery : IRequest<GetByGidPersonnelDocumentResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelDocumentQueryHandler : IRequestHandler<GetByGidPersonnelDocumentQuery, GetByGidPersonnelDocumentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelDocumentReadRepository _personnelDocumentReadRepository;
            private readonly PersonnelDocumentBusinessRules _personnelDocumentBusinessRules;

            public GetByGidPersonnelDocumentQueryHandler(IMapper mapper, IPersonnelDocumentReadRepository personnelDocumentReadRepository, PersonnelDocumentBusinessRules personnelDocumentBusinessRules)
            {
                _mapper = mapper;
                _personnelDocumentReadRepository = personnelDocumentReadRepository;
                _personnelDocumentBusinessRules = personnelDocumentBusinessRules;
            }

            public async Task<GetByGidPersonnelDocumentResponse> Handle(GetByGidPersonnelDocumentQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelDocument? personnelDocument = await _personnelDocumentReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.DocumentTypeFK));

                await _personnelDocumentBusinessRules.PersonnelDocumentShouldExistWhenSelected(personnelDocument);

                GetByGidPersonnelDocumentResponse response = _mapper.Map<GetByGidPersonnelDocumentResponse>(personnelDocument);
                return response;
            }
        }
    }
}