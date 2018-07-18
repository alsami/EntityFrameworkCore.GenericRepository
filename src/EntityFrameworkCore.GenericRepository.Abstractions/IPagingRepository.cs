using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Abstractions
{
    public interface IPagingRepository<TEntity, TId> where TEntity : class, IEntity<TId>, new() where TId : IEquatable<TId>
    {
        PagedModel<TEntity, TId> GetPaged(int page, int size, SortDirection sortDirection = SortDirection.Ascending);

        Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, SortDirection sortDirection = SortDirection.Ascending);
        PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName, 
            SortDirection sortDirection = SortDirection.Ascending);

        Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName, 
            SortDirection sortDirection = SortDirection.Ascending);

        PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName, 
            SortDirection sortDirection = SortDirection.Ascending, params Expression<Func<TEntity, object>>[] includes);

        Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName, 
            SortDirection sortDirection = SortDirection.Ascending, params Expression<Func<TEntity, object>>[] includes);

        PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName,
            SortDirection sortDirection,
            Expression<Func<TEntity, bool>> predicate);

        Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName,
            SortDirection sortDirection,
            Expression<Func<TEntity, bool>> predicate);

        PagedModel<TEntity, TId> GetPaged(int page, int size, string orderByPropertyName, 
            SortDirection sortDirection,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        Task<PagedModel<TEntity, TId>> GetPagedAsync(int page, int size, string orderByPropertyName,
            SortDirection sortDirection,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);
    }
}