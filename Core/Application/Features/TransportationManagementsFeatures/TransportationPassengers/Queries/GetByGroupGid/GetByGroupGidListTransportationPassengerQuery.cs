using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
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

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByGroupGid
{
    public class GetByGroupGidListTransportationPassengerQuery : IRequest<GetListResponse<GetByGroupGidListTransportationPassengerListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GroupGid { get; set; }
        public class GetByServiceGidListTransportationPassengerQueryHandler : IRequestHandler<GetByGroupGidListTransportationPassengerQuery, GetListResponse<GetByGroupGidListTransportationPassengerListItemDto>>
        {
            private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.TransportationPassenger, GetByGroupGidListTransportationPassengerListItemDto> _noPagination;

            public GetByServiceGidListTransportationPassengerQueryHandler(ITransportationPassengerReadRepository transportationPassengerReadRepository, IMapper mapper, NoPagination<X.TransportationPassenger, GetByGroupGidListTransportationPassengerListItemDto> noPagination)
            {
                _transportationPassengerReadRepository = transportationPassengerReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByGroupGidListTransportationPassengerListItemDto>> Handle(GetByGroupGidListTransportationPassengerQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTransportationGroupFK == request.GroupGid,
                        includes: new Expression<Func<TransportationPassenger, object>>[]
                        {
                           x => x.TransportationGroupFK,
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.TransportationPassenger> transportationPassengers = await _transportationPassengerReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidTransportationGroupFK == request.GroupGid,
                    include: x => x.Include(y => y.TransportationGroupFK)
                );

                GetListResponse<GetByGroupGidListTransportationPassengerListItemDto> response = _mapper.Map<GetListResponse<GetByGroupGidListTransportationPassengerListItemDto>>(transportationPassengers);
                return response;
            }
        }
    }
}
