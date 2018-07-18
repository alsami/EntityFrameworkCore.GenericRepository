using System;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Abstractions
{
    public interface ICommonEntityRepository<out TEntity, in TId> where TEntity : class, IEntity<TId>, new()
        where TId : IEquatable<TId>
    {
        IQueryable<TEntity> GetQueryAble(bool noTracking = false);

        int Count();

        Task<int> CountAsync();
    }
}