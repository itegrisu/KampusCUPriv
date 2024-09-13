using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetList;

public class GetListPersonnelPassportInfoQuery : IRequest<GetListResponse<GetListPersonnelPassportInfoListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelPassportInfoQueryHandler : IRequestHandler<GetListPersonnelPassportInfoQuery, GetListResponse<GetListPersonnelPassportInfoListItemDto>>
    {
        private readonly IPersonnelPassportInfoReadRepository _personnelPassportInfoReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelPassportInfo, GetListPersonnelPassportInfoListItemDto> _noPagination;

        public GetListPersonnelPassportInfoQueryHandler(IPersonnelPassportInfoReadRepository personnelPassportInfoReadRepository, IMapper mapper, NoPagination<X.PersonnelPassportInfo, GetListPersonnelPassportInfoListItemDto> noPagination)
        {
            _personnelPassportInfoReadRepository = personnelPassportInfoReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelPassportInfoListItemDto>> Handle(GetListPersonnelPassportInfoQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<PersonnelPassportInfo, object>>[]
                    {
                       x => x.UserFK,
                    });
            }


            IPaginate<X.PersonnelPassportInfo> personnelPassportInfos = await _personnelPassportInfoReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK)
            );

            GetListResponse<GetListPersonnelPassportInfoListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelPassportInfoListItemDto>>(personnelPassportInfos);
            return response;
        }
    }
}