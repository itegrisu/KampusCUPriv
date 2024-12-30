using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetList;

public class GetListPartTimeWorkerForeignLanguageQuery : IRequest<GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto>>
{
    public string PartTimeWorkerGid { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public class GetListPartTimeWorkerForeignLanguageQueryHandler : IRequestHandler<GetListPartTimeWorkerForeignLanguageQuery, GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto>>
    {
        private readonly IPartTimeWorkerForeignLanguageReadRepository _partTimeWorkerForeignLanguageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PartTimeWorkerForeignLanguage, GetListPartTimeWorkerForeignLanguageListItemDto> _noPagination;

        public GetListPartTimeWorkerForeignLanguageQueryHandler(IPartTimeWorkerForeignLanguageReadRepository partTimeWorkerForeignLanguageReadRepository, IMapper mapper, NoPagination<X.PartTimeWorkerForeignLanguage, GetListPartTimeWorkerForeignLanguageListItemDto> noPagination)
        {
            _partTimeWorkerForeignLanguageReadRepository = partTimeWorkerForeignLanguageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto>> Handle(GetListPartTimeWorkerForeignLanguageQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<X.PartTimeWorkerForeignLanguage, bool>> predicate = null;

            if (request.PartTimeWorkerGid != null)
                predicate = x => x.GidPartTimeWorkerFK.ToString() == request.PartTimeWorkerGid;

            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
                    includes: new Expression<Func<PartTimeWorkerForeignLanguage, object>>[]
                    {
                       x => x.ForeignLanguageFK
                    });



            IPaginate<X.PartTimeWorkerForeignLanguage> partTimeWorkerForeignLanguages = await _partTimeWorkerForeignLanguageReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                include: x => x.Include(x => x.ForeignLanguageFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto> response = _mapper.Map<GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto>>(partTimeWorkerForeignLanguages);
            return response;
        }
    }
}