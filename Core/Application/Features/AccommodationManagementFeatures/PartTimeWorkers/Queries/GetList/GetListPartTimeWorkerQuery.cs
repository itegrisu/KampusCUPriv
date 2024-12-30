using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Enum;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetList;

public class GetListPartTimeWorkerQuery : IRequest<GetListResponse<GetListPartTimeWorkerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPartTimeWorkerQueryHandler : IRequestHandler<GetListPartTimeWorkerQuery, GetListResponse<GetListPartTimeWorkerListItemDto>>
    {
        private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PartTimeWorker, GetListPartTimeWorkerListItemDto> _noPagination;

        public GetListPartTimeWorkerQueryHandler(IPartTimeWorkerReadRepository partTimeWorkerReadRepository, IMapper mapper, NoPagination<X.PartTimeWorker, GetListPartTimeWorkerListItemDto> noPagination)
        {
            _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPartTimeWorkerListItemDto>> Handle(GetListPartTimeWorkerQuery request, CancellationToken cancellationToken)
        {
            GetListResponse<GetListPartTimeWorkerListItemDto> response;

            if (request.PageRequest.PageIndex == -1)
            {
                var partTimeWorkers = await _noPagination.NoPaginationData(cancellationToken);

                // Foreign languages için Languages özelliðini doldur
                foreach (var item in partTimeWorkers.Items)
                {
                    var worker = await _partTimeWorkerReadRepository.GetAsync(
                        x => x.Gid == item.Gid,
                        include: source => source.Include(p => p.PartTimeWorkerForeignLanguages)
                                                 .ThenInclude(pfl => pfl.ForeignLanguageFK)
                    );

                    if (worker?.PartTimeWorkerForeignLanguages != null)
                    {
                        var activeLanguages = worker.PartTimeWorkerForeignLanguages
                           .Where(x => x.DataState == DataState.Active)
                           .Select(pfl => pfl.ForeignLanguageFK.LanguageCode)
                           .Where(code => !string.IsNullOrEmpty(code));
                        item.Languages = string.Join(", ", activeLanguages);
                    }
                }

                response = partTimeWorkers;
            }
            else
            {
                IPaginate<X.PartTimeWorker> partTimeWorkers = await _partTimeWorkerReadRepository.GetListAsync(
                    include: source => source.Include(p => p.PartTimeWorkerForeignLanguages)
                                             .ThenInclude(pfl => pfl.ForeignLanguageFK), // ForeignLanguage bilgisi ekleniyor
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                response = _mapper.Map<GetListResponse<GetListPartTimeWorkerListItemDto>>(partTimeWorkers);

                // Foreign languages için Languages özelliðini doldur
                foreach (var item in response.Items)
                {

                    var worker = partTimeWorkers.Items.FirstOrDefault(w => w.Gid == item.Gid);
                    if (worker?.PartTimeWorkerForeignLanguages != null)
                    {
                        var activeLanguages = worker.PartTimeWorkerForeignLanguages
                          .Where(x => x.DataState == DataState.Active)
                          .Select(pfl => pfl.ForeignLanguageFK.LanguageCode)
                          .Where(code => !string.IsNullOrEmpty(code));
                        item.Languages = string.Join(", ", activeLanguages);
                    }
                }
            }
            return response;
        }
    }
}