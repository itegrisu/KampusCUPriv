using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetList;

public class GetListPartTimeWorkerForeignLanguageQuery : IRequest<GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto>>
{
    public PageRequest PageRequest { get; set; }

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
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<PartTimeWorkerForeignLanguage, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.PartTimeWorkerForeignLanguageMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.PartTimeWorkerForeignLanguage> partTimeWorkerForeignLanguages = await _partTimeWorkerForeignLanguageReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto> response = _mapper.Map<GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto>>(partTimeWorkerForeignLanguages);
            return response;
        }
    }
}