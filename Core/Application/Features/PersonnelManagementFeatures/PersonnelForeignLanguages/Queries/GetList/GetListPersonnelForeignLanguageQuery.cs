using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetList;

public class GetListPersonnelForeignLanguageQuery : IRequest<GetListResponse<GetListPersonnelForeignLanguageListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelForeignLanguageQueryHandler : IRequestHandler<GetListPersonnelForeignLanguageQuery, GetListResponse<GetListPersonnelForeignLanguageListItemDto>>
    {
        private readonly IPersonnelForeignLanguageReadRepository _personnelForeignLanguageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelForeignLanguage, GetListPersonnelForeignLanguageListItemDto> _noPagination;

        public GetListPersonnelForeignLanguageQueryHandler(IPersonnelForeignLanguageReadRepository personnelForeignLanguageReadRepository, IMapper mapper, NoPagination<X.PersonnelForeignLanguage, GetListPersonnelForeignLanguageListItemDto> noPagination)
        {
            _personnelForeignLanguageReadRepository = personnelForeignLanguageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelForeignLanguageListItemDto>> Handle(GetListPersonnelForeignLanguageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<PersonnelForeignLanguage, object>>[]
                   {
                       x => x.UserFK,
                       x=> x.ForeignLanguageFK
                   });
            }

            IPaginate<X.PersonnelForeignLanguage> personnelForeignLanguages = await _personnelForeignLanguageReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK).Include(x => x.ForeignLanguageFK)
            );

            GetListResponse<GetListPersonnelForeignLanguageListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelForeignLanguageListItemDto>>(personnelForeignLanguages);
            return response;
        }
    }
}