namespace EntityFrameworkCore.GenericRepository.Shared
{
    public abstract class StringEntity : IEntity<string>
    {
        public virtual string Id { get; set; }
    }
}