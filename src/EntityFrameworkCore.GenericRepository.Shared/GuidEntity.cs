using System;

namespace EntityFrameworkCore.GenericRepository.Shared
{
    public abstract class GuidEntity : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
    }
}