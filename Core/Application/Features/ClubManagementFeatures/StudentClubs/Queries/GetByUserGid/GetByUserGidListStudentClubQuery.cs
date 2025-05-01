using Application.Features.ClubFeatures.StudentClubs.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.ClubManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubManagementFeatures.StudentClubs.Queries.GetByUserGid
{
    public class GetByUserGidListStudentClubQuery : IRequest<GetListResponse<GetByUserGidListStudentClubListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListStudentClubQueryHandler : IRequestHandler<GetByUserGidListStudentClubQuery, GetListResponse<GetByUserGidListStudentClubListItemDto>>
        {
            private readonly IStudentClubReadRepository _studentClubReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.StudentClub, GetByUserGidListStudentClubListItemDto> _noPagination;

            public GetByUserGidListStudentClubQueryHandler(IStudentClubReadRepository studentClubReadRepository, IMapper mapper, NoPagination<X.StudentClub, GetByUserGidListStudentClubListItemDto> noPagination)
            {
                _studentClubReadRepository = studentClubReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListStudentClubListItemDto>> Handle(GetByUserGidListStudentClubQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidUserFK== request.UserGid && x.DataState == Core.Enum.DataState.Active,
                        orderBy: x => x.ClubFK.Name,
                        includes: new Expression<Func<StudentClub, object>>[]
                        {
                       x => x.UserFK,
                       x => x.ClubFK
                        });
                IPaginate<X.StudentClub> studentClubs = await _studentClubReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.UserFK).Include(x => x.ClubFK),
                    predicate: x => x.GidUserFK == request.UserGid && x.DataState == Core.Enum.DataState.Active,
                    orderBy: x => x.OrderBy(x => x.ClubFK.Name)
                );

                GetListResponse<GetByUserGidListStudentClubListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListStudentClubListItemDto>>(studentClubs);
                return response;
            }
        }
    }
}
