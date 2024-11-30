using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Queries.GetByServiceGid
{
    public class GetByServiceGidListTransportationPersonnelQuery : IRequest<GetListResponse<GetByServiceGidListTransportationPersonnelListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid ServiceGid { get; set; }
        public class GetByServiceGidListTransportationPersonnelQueryHandler : IRequestHandler<GetByServiceGidListTransportationPersonnelQuery, GetListResponse<GetByServiceGidListTransportationPersonnelListItemDto>>
        {
            private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.TransportationPersonnel, GetByServiceGidListTransportationPersonnelListItemDto> _noPagination;

            public GetByServiceGidListTransportationPersonnelQueryHandler(ITransportationPersonnelReadRepository transportationPersonnelReadRepository, IMapper mapper, NoPagination<X.TransportationPersonnel, GetByServiceGidListTransportationPersonnelListItemDto> noPagination)
            {
                _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByServiceGidListTransportationPersonnelListItemDto>> Handle(GetByServiceGidListTransportationPersonnelQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTransportationServiceFK == request.ServiceGid,
                        includes: new Expression<Func<TransportationPersonnel, object>>[]
                        {
                       x => x.TransportationServiceFK,
                       x=> x.UserFK
                        });
                IPaginate<X.TransportationPersonnel> transportationPersonnels = await _transportationPersonnelReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidTransportationServiceFK == request.ServiceGid,
                    include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK)
                );

                GetListResponse<GetByServiceGidListTransportationPersonnelListItemDto> response = _mapper.Map<GetListResponse<GetByServiceGidListTransportationPersonnelListItemDto>>(transportationPersonnels);
                return response;
            }
        }
    }
}
