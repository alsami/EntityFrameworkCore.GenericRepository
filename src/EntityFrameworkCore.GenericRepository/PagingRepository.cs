using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.Base;
using EntityFrameworkCore.GenericRepository.Extensions;
using EntityFrameworkCore.GenericRepository.Shared;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.GenericRepository
{
    public class PagingRepository<TEntity, TId> : CommonEntityRepository<TEntity, TId>, IPagingRepository<TEntity, TId> where TEntity : class, IEntity<TId>, new() where TId : IEquatable<TId>
    {
        private readonly GenericRepositoryContext context;
        
        private const string IdPropertyName = "Id";
        
        public PagingRepository(GenericRepositoryContext context) : base(context)
        {
            this.context = context;
        }

        public virtual PagedModel<TEntity, TId> GetPaged(int page, int size, SortDirection sortDirection = SortDirection.Ascending)
        {
            var totalCount = this.Count();

            var entities = this.CreateQuery()
                .ApplyOrder<TEntity, TId>(PagingRepository<TEntity, TId>.IdPropertyName, GetOrderMethodName(sortDirection))
                .OrderBy(entity => entity.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToArray();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, PagingRepository<TEntity, TId>.IdPropertyName, sortDirection);
        }

        public virtual async Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, SortDirection sortDirection = SortDirection.Ascending)
        {
            var totalCount = await this.CountAsync();

            var entities = await this.CreateQuery()
                .ApplyOrder<TEntity, TId>(PagingRepository<TEntity, TId>.IdPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArrayAsync();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, PagingRepository<TEntity, TId>.IdPropertyName, sortDirection);
        }

        public virtual PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName,
            SortDirection sortDirection = SortDirection.Ascending)
        {
            var totalCount = this.Count();

            var entities = this.CreateQuery()
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArray();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, orderByPropertyName, sortDirection);
        }

        public async Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName,
            SortDirection sortDirection = SortDirection.Ascending)
        {
            var totalCount = await this.CountAsync();

            var entities = await this.CreateQuery()
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArrayAsync();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, orderByPropertyName, sortDirection);
        }

        public PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName,
            SortDirection sortDirection = SortDirection.Ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            var totalCount = this.Count();

            var entities = this.BuildIncludes(includes)
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArray();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, orderByPropertyName, sortDirection);
        }

        public async Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName,
            SortDirection sortDirection = SortDirection.Ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            var totalCount = await this.CountAsync();

            var entities = await this.BuildIncludes(includes)
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArrayAsync();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, PagingRepository<TEntity, TId>.IdPropertyName, sortDirection);
        }

        public PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName, SortDirection sortDirection, Expression<Func<TEntity, bool>> predicate)
        {
            var totalCount = this.Count();

            var entities = this.CreateQuery()
                .Where(predicate)
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArray();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, PagingRepository<TEntity, TId>.IdPropertyName, sortDirection);
        }

        public async Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName, SortDirection sortDirection, Expression<Func<TEntity, bool>> predicate)
        {
            var totalCount = await this.CountAsync();

            var entities = await this.BuildIncludes()
                .Where(predicate)
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArrayAsync();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, PagingRepository<TEntity, TId>.IdPropertyName, sortDirection);
        }

        public PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName, SortDirection sortDirection, 
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var totalCount = this.Count();

            var entities = this.BuildIncludes(includes)
                .Where(predicate)
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArray();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, PagingRepository<TEntity, TId>.IdPropertyName, sortDirection);
        }

        public async Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName, SortDirection sortDirection,
            Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var totalCount = await this.CountAsync();

            var entities = await this.BuildIncludes(includes)
                .Where(predicate)
                .ApplyOrder<TEntity, TId>(orderByPropertyName, GetOrderMethodName(sortDirection))
                .Skip((page - 1) * size)
                .Take(size)
                .ToArrayAsync();

            return new PagedModel<TEntity, TId>(entities, page, size, totalCount, PagingRepository<TEntity, TId>.IdPropertyName, sortDirection);
        }
        
        private static string GetOrderMethodName(SortDirection sortDirection)
        {
            return !Enum.IsDefined(typeof(SortDirection), sortDirection)
                ? QueryableExtensions.OrderByMethodName
                : sortDirection == SortDirection.Ascending
                    ? QueryableExtensions.OrderByMethodName
                    : sortDirection == SortDirection.Descending
                        ? QueryableExtensions.OrderByDescendingMethodName
                        : sortDirection == SortDirection.ThenByAscending
                            ? QueryableExtensions.ThenOrderByMethodName
                            : QueryableExtensions.ThenOrderByDescendingMethodName;
        }
    }
}