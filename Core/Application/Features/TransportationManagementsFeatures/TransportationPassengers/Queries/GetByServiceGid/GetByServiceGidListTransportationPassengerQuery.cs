using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByGroupGid;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByServiceGid
{
    public class GetByServiceGidListTransportationPassengerQuery : IRequest<GetListResponse<GetByServiceGidListTransportationPassengerListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid ServiceGid { get; set; }
        public class GetByServiceGidListTransportationPassengerQueryHandler : IRequestHandler<GetByServiceGidListTransportationPassengerQuery, GetListResponse<GetByServiceGidListTransportationPassengerListItemDto>>
        {
            private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
            private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.TransportationPassenger, GetByServiceGidListTransportationPassengerListItemDto> _noPagination;

            public GetByServiceGidListTransportationPassengerQueryHandler(ITransportationPassengerReadRepository transportationPassengerReadRepository, IMapper mapper, NoPagination<X.TransportationPassenger, GetByServiceGidListTransportationPassengerListItemDto> noPagination, ITransportationGroupReadRepository transportationGroupReadRepository)
            {
                _transportationPassengerReadRepository = transportationPassengerReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _transportationGroupReadRepository = transportationGroupReadRepository;
            }

            public async Task<GetListResponse<GetByServiceGidListTransportationPassengerListItemDto>> Handle(GetByServiceGidListTransportationPassengerQuery request, CancellationToken cancellationToken)
            {
                var getGroupGid = await _transportationGroupReadRepository.GetSingleAsync(x => x.GidTransportationServiceFK == request.ServiceGid);
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTransportationGroupFK == getGroupGid.Gid,
                        includes: new Expression<Func<TransportationPassenger, object>>[]
                        {
                           x => x.TransportationGroupFK,
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.TransportationPassenger> transportationPassengers = await _transportationPassengerReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidTransportationGroupFK == getGroupGid.Gid,
                    include: x => x.Include(y => y.TransportationGroupFK)
                );

                GetListResponse<GetByServiceGidListTransportationPassengerListItemDto> response = _mapper.Map<GetListResponse<GetByServiceGidListTransportationPassengerListItemDto>>(transportationPassengers);
                return response;
            }
        }
    }

}
