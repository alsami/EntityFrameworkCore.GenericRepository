namespace EntityFrameworkCore.GenericRepository.Shared
{
    public abstract class LongEntity : IEntity<long>
    {
        public virtual long Id { get; set; }
    }
}