using System;

namespace EntityFrameworkCore.GenericRepository.Shared
{
    public interface IEntity<out TId> where TId : IEquatable<TId>
    {
        TId Id { get; }
    }
}