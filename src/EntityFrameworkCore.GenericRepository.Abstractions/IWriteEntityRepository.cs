using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Abstractions
{
    public interface IWriteEntityRepository<in TEntity, TId> where TEntity : class, IEntity<TId>, new() where TId : IEquatable<TId>
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void AddMany(IEnumerable<TEntity> entities);

        Task AddManyAsync(IEnumerable<TEntity> entities);

        void Edit(TEntity entity);

        void Delete(TEntity entity);

        bool EnsureChanges();

        Task<bool> EnsureChangesAsync();
    }
}