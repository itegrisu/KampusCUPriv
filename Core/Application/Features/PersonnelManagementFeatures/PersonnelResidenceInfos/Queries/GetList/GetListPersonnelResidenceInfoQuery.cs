using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetList;

public class GetListPersonnelResidenceInfoQuery : IRequest<GetListResponse<GetListPersonnelResidenceInfoListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelResidenceInfoQueryHandler : IRequestHandler<GetListPersonnelResidenceInfoQuery, GetListResponse<GetListPersonnelResidenceInfoListItemDto>>
    {
        private readonly IPersonnelResidenceInfoReadRepository _personnelResidenceInfoReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelResidenceInfo, GetListPersonnelResidenceInfoListItemDto> _noPagination;

        public GetListPersonnelResidenceInfoQueryHandler(IPersonnelResidenceInfoReadRepository personnelResidenceInfoReadRepository, IMapper mapper, NoPagination<X.PersonnelResidenceInfo, GetListPersonnelResidenceInfoListItemDto> noPagination)
        {
            _personnelResidenceInfoReadRepository = personnelResidenceInfoReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelResidenceInfoListItemDto>> Handle(GetListPersonnelResidenceInfoQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<PersonnelResidenceInfo, object>>[]
                    {
                       x => x.UserFK,
                    });
            }


            IPaginate<X.PersonnelResidenceInfo> personnelResidenceInfos = await _personnelResidenceInfoReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK));

            GetListResponse<GetListPersonnelResidenceInfoListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelResidenceInfoListItemDto>>(personnelResidenceInfos);
            return response;
        }
    }
}