using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetList;

public class GetListJobTypeQuery : IRequest<GetListResponse<GetListJobTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListJobTypeQueryHandler : IRequestHandler<GetListJobTypeQuery, GetListResponse<GetListJobTypeListItemDto>>
    {
        private readonly IJobTypeReadRepository _jobTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.JobType, GetListJobTypeListItemDto> _noPagination;

        public GetListJobTypeQueryHandler(IJobTypeReadRepository jobTypeReadRepository, IMapper mapper, NoPagination<X.JobType, GetListJobTypeListItemDto> noPagination)
        {
            _jobTypeReadRepository = jobTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListJobTypeListItemDto>> Handle(GetListJobTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);


            IPaginate<X.JobType> jobTypes = await _jobTypeReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListJobTypeListItemDto> response = _mapper.Map<GetListResponse<GetListJobTypeListItemDto>>(jobTypes);
            return response;
        }
    }
}