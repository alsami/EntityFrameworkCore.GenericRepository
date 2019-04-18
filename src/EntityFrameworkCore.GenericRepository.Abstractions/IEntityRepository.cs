using System;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Abstractions
{
    public interface IEntityRepository<TEntity, TId> : 
        IReadEntityRepository<TEntity, TId>,
        IWriteEntityRepository<TEntity, TId>,
        ICommonEntityRepository<TEntity, TId> where TEntity : class, IEntity<TId>, new() where TId : IEquatable<TId>
    {
        
    }
}