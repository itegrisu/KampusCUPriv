using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Responses;
using Core.Entities;
using Core.Persistence.Paging;
using Core.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Helpers.PaginationHelpers
{
    public class NoPagination<TEntity, TDto>
        where TEntity : BaseEntity, new()
        where TDto : IDto
    {
        private readonly IMapper _mapper;
        private readonly IReadRepository<TEntity> _readRepository;

        public NoPagination(IMapper mapper, IReadRepository<TEntity> readRepository)
        {
            _mapper = mapper;
            _readRepository = readRepository;
        }

        public async Task<GetListResponse<TDto>> NoPaginationAllData(
          CancellationToken cancellationToken,
           Expression<Func<TEntity, bool>> predicate = null,
           Expression<Func<TEntity, object>> orderBy = null,
           Expression<Func<TEntity, object>> orderByDesc = null,
           params Expression<Func<TEntity, object>>[] includes)
        {

            int count = _readRepository.CountFull();

            IPaginate<TEntity> fullData;

            if (includes != null && includes.Any())
            {
                fullData = await _readRepository.GetListAllAsync(
                    predicate: predicate,
                    orderBy: orderBy != null ? o => o.OrderBy(orderBy) : null,
                     orderByDesc: orderByDesc != null ? o => o.OrderByDescending(orderByDesc) : null,
                    include: query =>
                    {
                        foreach (var include in includes)
                        {
                            query = query.Include(include);
                        }
                        return (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<TEntity, object>)query;
                    },
                    index: 0,
                    size: count,
                    cancellationToken: cancellationToken);
            }
            else
            {

                fullData = await _readRepository.GetListAllAsync(
                    predicate: predicate,
                    orderBy: orderBy != null ? o => o.OrderBy(orderBy) : null,
                    orderByDesc: orderByDesc != null ? o => o.OrderByDescending(orderByDesc) : null,
                    include: null,
                    index: 0,
                    size: count,
                    cancellationToken: cancellationToken);
            }


            GetListResponse<TDto> responseFull = _mapper.Map<GetListResponse<TDto>>(fullData);
            return responseFull;
        }


        public async Task<GetListResponse<TDto>> NoPaginationData(
         CancellationToken cancellationToken,
          Expression<Func<TEntity, bool>> predicate = null,
          Expression<Func<TEntity, object>> orderBy = null,
          Expression<Func<TEntity, object>> orderByDesc = null,
          bool isDesc = false,
          params Expression<Func<TEntity, object>>[] includes)
        {

            int count = _readRepository.CountFull();

            IPaginate<TEntity> fullData;

            if (includes != null && includes.Any())
            {
                fullData = await _readRepository.GetListAsync(
                    predicate: predicate,
                    orderBy: orderBy != null ? o => o.OrderBy(orderBy) : null,
                    orderByDesc: orderByDesc != null ? o => o.OrderByDescending(orderByDesc) : null,
                    include: query =>
                    {
                        foreach (var include in includes)
                        {
                            query = query.Include(include);
                        }
                        return (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<TEntity, object>)query;
                    },
                    index: 0,
                    size: count,
                    cancellationToken: cancellationToken);
            }
            else
            {

                fullData = await _readRepository.GetListAsync(
                    predicate: predicate,
                    orderBy: orderBy != null ? o => o.OrderBy(orderBy) : null,
                    orderByDesc: orderByDesc != null ? o => o.OrderByDescending(orderByDesc) : null,
                    include: null,
                    index: 0,
                    size: count,
                    cancellationToken: cancellationToken);
            }


            GetListResponse<TDto> responseFull = _mapper.Map<GetListResponse<TDto>>(fullData);
            return responseFull;
        }
    }
}
