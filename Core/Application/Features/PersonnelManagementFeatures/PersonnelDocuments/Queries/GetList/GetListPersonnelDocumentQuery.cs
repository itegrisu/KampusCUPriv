using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using System.Linq.Expressions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetList;

public class GetListPersonnelDocumentQuery : IRequest<GetListResponse<GetListPersonnelDocumentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelDocumentQueryHandler : IRequestHandler<GetListPersonnelDocumentQuery, GetListResponse<GetListPersonnelDocumentListItemDto>>
    {
        private readonly IPersonnelDocumentReadRepository _personnelDocumentReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelDocument, GetListPersonnelDocumentListItemDto> _noPagination;

        public GetListPersonnelDocumentQueryHandler(IPersonnelDocumentReadRepository personnelDocumentReadRepository, IMapper mapper, NoPagination<X.PersonnelDocument, GetListPersonnelDocumentListItemDto> noPagination)
        {
            _personnelDocumentReadRepository = personnelDocumentReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelDocumentListItemDto>> Handle(GetListPersonnelDocumentQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<PersonnelDocument, object>>[]
                   {
                       x => x.UserFK,
                       x=> x.DocumentTypeFK
                   });
            }

            IPaginate<X.PersonnelDocument> personnelDocuments = await _personnelDocumentReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPersonnelDocumentListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelDocumentListItemDto>>(personnelDocuments);
            return response;
        }
    }
}