using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Abstractions
{
    public interface IReadEntityRepository<TEntity, in TId> where TEntity : class, IEntity<TId>, new()
        where TId : IEquatable<TId>
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TType> GetAll<TType>(Expression<Func<TEntity, TType>> projectToFunc) where TType : class;

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TType>> GetAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc) where TType : class;

        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TType> GetAll<TType>(Expression<Func<TEntity, TType>> projectToFunc,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;

        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TType>> GetAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TType> FindAll<TType>(Expression<Func<TEntity, TType>> projectToFunc,
            Expression<Func<TEntity, bool>> predicate) where TType : class;

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TType>> FindAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc,
            Expression<Func<TEntity, bool>> predicate) where TType : class;

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TType> FindAll<TType>(Expression<Func<TEntity, TType>> projectToFunc, Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TType>> FindAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;

        TEntity Find(TId key);

        Task<TEntity> FindAsync(TId key);

        TEntity Find(TId key, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindAsync(TId key, params Expression<Func<TEntity, object>>[] includes);

        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        bool HasMatching(Expression<Func<TEntity, bool>> predicate);

        Task<bool> HasMatchingAsync(Expression<Func<TEntity, bool>> predicate);
    }
}