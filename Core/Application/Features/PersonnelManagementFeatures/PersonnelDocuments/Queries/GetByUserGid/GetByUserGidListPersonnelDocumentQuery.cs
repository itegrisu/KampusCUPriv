using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.PersonnelManagements;


namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelDocumentQuery : IRequest<GetListResponse<GetByUserGidListPersonnelDocumentListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListPersonnelDocumentQueryHandler : IRequestHandler<GetByUserGidListPersonnelDocumentQuery, GetListResponse<GetByUserGidListPersonnelDocumentListItemDto>>
        {
            private readonly IPersonnelDocumentReadRepository _personnelDocumentReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelDocument, GetByUserGidListPersonnelDocumentListItemDto> _noPagination;

            public GetByUserGidListPersonnelDocumentQueryHandler(IPersonnelDocumentReadRepository personnelDocumentReadRepository, IMapper mapper, NoPagination<X.PersonnelDocument, GetByUserGidListPersonnelDocumentListItemDto> noPagination)
            {
                _personnelDocumentReadRepository = personnelDocumentReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelDocumentListItemDto>> Handle(GetByUserGidListPersonnelDocumentQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       predicate: x => x.GidPersonnelFK == request.UserGid,
                       includes: new Expression<Func<PersonnelDocument, object>>[]
                       {
                       x => x.UserFK,
                       x=> x.DocumentTypeFK
                       });
                }

                IPaginate<X.PersonnelDocument> personnelDocuments = await _personnelDocumentReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetByUserGidListPersonnelDocumentListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelDocumentListItemDto>>(personnelDocuments);
                return response;
            }
        }
    }
}
