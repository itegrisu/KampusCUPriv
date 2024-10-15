using Application.Features.GeneralManagementFeatures.Departments.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.GeneralManagements;


namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetListWithUser
{
    public class GetListWithUserDepartmentQuery : IRequest<GetListResponse<GetListWithUserDepartmentListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListWithUserDepartmentQueryHandler : IRequestHandler<GetListWithUserDepartmentQuery, GetListResponse<GetListWithUserDepartmentListItemDto>>
        {
            private readonly IDepartmentReadRepository _departmentReadRepository;
            private readonly IDepartmentUserReadRepository _departmentUserReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Department, GetListWithUserDepartmentListItemDto> _noPagination;

            public GetListWithUserDepartmentQueryHandler(IDepartmentReadRepository departmentReadRepository, IMapper mapper, NoPagination<X.Department, GetListWithUserDepartmentListItemDto> noPagination, IDepartmentUserReadRepository departmentUserReadRepository)
            {
                _departmentReadRepository = departmentReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _departmentUserReadRepository = departmentUserReadRepository;
            }

            public async Task<GetListResponse<GetListWithUserDepartmentListItemDto>> Handle(GetListWithUserDepartmentQuery request, CancellationToken cancellationToken)
            {
                // DepartmentUser listesini alıyoruz
                List<DepartmentUser> departmentUsers = await _departmentUserReadRepository.GetAll()
                    .Include(x => x.UserFK)
                    .Include(x => x.DepartmentFK)
                    .ToListAsync(cancellationToken);

                // DTO listesi oluşturuluyor
                var departmentUserListItems = new List<GetListWithUserDepartmentListItemDto>();

                // Kullanıcıları departmanlarına göre grupluyoruz
                var groupedDepartmentUsers = departmentUsers
                    .GroupBy(x => x.GidDepartmentFK)
                    .ToList();

                foreach (var group in groupedDepartmentUsers)
                {
                    // Her departman için kullanıcı sayısını hesaplıyoruz
                    var userCount = group.Count();

                    // DTO oluşturuyoruz
                    var dto = new GetListWithUserDepartmentListItemDto
                    {
                        Gid = group.Key,
                        Name = group.First().DepartmentFK.Name, // Departman adı
                        UserCount = userCount, // Kullanıcı sayısı
                        MainAdminFKFullName = group.First().DepartmentFK.MainAdminFK?.FullName ?? "",
                        GidMainAdminFK = group.First().DepartmentFK.MainAdminFK?.Gid ?? Guid.Empty,
                        CoAdminFKFullName = group.First().DepartmentFK.CoAdminFK?.FullName ?? "",
                        GidCoAdminFK = group.First().DepartmentFK.CoAdminFK?.Gid
                    };

                    // DTO'yu listeye ekliyoruz
                    departmentUserListItems.Add(dto);
                }

                // Response'yi manuel olarak oluşturuyoruz
                return new GetListResponse<GetListWithUserDepartmentListItemDto>
                {
                    Items = departmentUserListItems,  // Hesaplanan liste
                    Count = departmentUserListItems.Count,
                    HasNext = false,   // Bu örnekte sayfalama yoksa
                    HasPrevious = false,
                    Index = 0,   // Eğer -1 gelirse tüm liste
                    Pages = 1,   // Sayfa sayısı
                    Size = departmentUserListItems.Count // Sayfa boyutu (hepsi için tek sayfa)
                };
            }

        }
    }
}
