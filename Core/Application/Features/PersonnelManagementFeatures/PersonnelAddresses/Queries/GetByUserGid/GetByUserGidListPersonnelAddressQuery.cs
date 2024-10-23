using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
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


namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelAddressQuery : IRequest<GetListResponse<GetByUserGidListPersonnelAddressListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid {  get; set; }
        public class GetByUserGidListPersonnelAddressQueryHandler : IRequestHandler<GetByUserGidListPersonnelAddressQuery, GetListResponse<GetByUserGidListPersonnelAddressListItemDto>>
        {
            private readonly IPersonnelAddressReadRepository _personnelAddressReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelAddress, GetByUserGidListPersonnelAddressListItemDto> _noPagination;

            public GetByUserGidListPersonnelAddressQueryHandler(IPersonnelAddressReadRepository personnelAddressReadRepository, IMapper mapper, NoPagination<X.PersonnelAddress, GetByUserGidListPersonnelAddressListItemDto> noPagination)
            {
                _personnelAddressReadRepository = personnelAddressReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelAddressListItemDto>> Handle(GetByUserGidListPersonnelAddressQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidPersonnelFK == request.UserGid, 
                        includes: new Expression<Func<PersonnelAddress, object>>[]
                        {
                           x => x.UserFK,
                           x=> x.CityFK
                        });
                //  return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.PersonnelAddress> personnelAddresss = await _personnelAddressReadRepository.GetListAllAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetByUserGidListPersonnelAddressListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelAddressListItemDto>>(personnelAddresss);
                return response;
            }
        }
    }
}
