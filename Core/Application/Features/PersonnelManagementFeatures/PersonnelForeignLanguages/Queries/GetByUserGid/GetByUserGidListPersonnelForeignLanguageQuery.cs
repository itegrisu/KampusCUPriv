using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelForeignLanguageQuery : IRequest<GetListResponse<GetByUserGidListPersonnelForeignLanguageListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListPersonnelForeignLanguageQueryHandler : IRequestHandler<GetByUserGidListPersonnelForeignLanguageQuery, GetListResponse<GetByUserGidListPersonnelForeignLanguageListItemDto>>
        {
            private readonly IPersonnelForeignLanguageReadRepository _personnelForeignLanguageReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelForeignLanguage, GetByUserGidListPersonnelForeignLanguageListItemDto> _noPagination;

            public GetByUserGidListPersonnelForeignLanguageQueryHandler(IPersonnelForeignLanguageReadRepository personnelForeignLanguageReadRepository, IMapper mapper, NoPagination<X.PersonnelForeignLanguage, GetByUserGidListPersonnelForeignLanguageListItemDto> noPagination)
            {
                _personnelForeignLanguageReadRepository = personnelForeignLanguageReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelForeignLanguageListItemDto>> Handle(GetByUserGidListPersonnelForeignLanguageQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       predicate: x => x.GidPersonnelFK == request.UserGid,
                       includes: new Expression<Func<PersonnelForeignLanguage, object>>[]
                       {
                       x => x.UserFK,
                       x=> x.ForeignLanguageFK
                       });
                }

                IPaginate<X.PersonnelForeignLanguage> personnelForeignLanguages = await _personnelForeignLanguageReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    include: x => x.Include(x => x.UserFK).Include(x => x.ForeignLanguageFK)
                );

                GetListResponse<GetByUserGidListPersonnelForeignLanguageListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelForeignLanguageListItemDto>>(personnelForeignLanguages);
                return response;
            }
        }
    }
}
