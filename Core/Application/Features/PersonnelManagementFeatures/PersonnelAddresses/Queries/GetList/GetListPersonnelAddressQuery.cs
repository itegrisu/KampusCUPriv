using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetList;

public class GetListPersonnelAddressQuery : IRequest<GetListResponse<GetListPersonnelAddressListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelAddressQueryHandler : IRequestHandler<GetListPersonnelAddressQuery, GetListResponse<GetListPersonnelAddressListItemDto>>
    {
        private readonly IPersonnelAddressReadRepository _personnelAddressReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelAddress, GetListPersonnelAddressListItemDto> _noPagination;

        public GetListPersonnelAddressQueryHandler(IPersonnelAddressReadRepository personnelAddressReadRepository, IMapper mapper, NoPagination<X.PersonnelAddress, GetListPersonnelAddressListItemDto> noPagination)
        {
            _personnelAddressReadRepository = personnelAddressReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelAddressListItemDto>> Handle(GetListPersonnelAddressQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<PersonnelAddress, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.PersonnelAddressMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.PersonnelAddress> personnelAddresss = await _personnelAddressReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPersonnelAddressListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelAddressListItemDto>>(personnelAddresss);
            return response;
        }
    }
}