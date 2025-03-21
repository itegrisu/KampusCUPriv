using Application.Features.ClubManagementFeatures.StudentClubs.Queries.GetByUserGid;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using AutoMapper;
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

namespace Application.Features.ClubManagementFeatures.StudentClubs.Queries.GetByClubGid
{
    public class GetByClubGidListStudentClubQuery : IRequest<GetListResponse<GetByClubGidListStudentClubListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid ClubGid { get; set; }
        public class GetByClubGidListStudentClubQueryHandler : IRequestHandler<GetByClubGidListStudentClubQuery, GetListResponse<GetByClubGidListStudentClubListItemDto>>
        {
            private readonly IStudentClubReadRepository _studentClubReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.StudentClub, GetByClubGidListStudentClubListItemDto> _noPagination;

            public GetByClubGidListStudentClubQueryHandler(IStudentClubReadRepository studentClubReadRepository, IMapper mapper, NoPagination<X.StudentClub, GetByClubGidListStudentClubListItemDto> noPagination)
            {
                _studentClubReadRepository = studentClubReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByClubGidListStudentClubListItemDto>> Handle(GetByClubGidListStudentClubQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidClubFK == request.ClubGid && x.DataState == Core.Enum.DataState.Active,
                        orderBy: x => x.ClubFK.Name,
                        includes: new Expression<Func<StudentClub, object>>[]
                        {
                           x => x.UserFK,
                           x => x.ClubFK,
                           x => x.UserFK.ClassFK,
                           x => x.UserFK.DepartmentFK
                        });
                IPaginate<X.StudentClub> studentClubs = await _studentClubReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.UserFK).Include(x => x.ClubFK).Include(x => x.UserFK).ThenInclude(x => x.ClassFK).Include(x => x.UserFK).ThenInclude(x => x.DepartmentFK),
                    predicate: x => x.GidClubFK == request.ClubGid && x.DataState == Core.Enum.DataState.Active,
                    orderBy: x => x.OrderBy(x => x.ClubFK.Name)
                );

                GetListResponse<GetByClubGidListStudentClubListItemDto> response = _mapper.Map<GetListResponse<GetByClubGidListStudentClubListItemDto>>(studentClubs);
                return response;
            }
        }
    }

}
