namespace EntityFrameworkCore.GenericRepository.Shared
{
    public abstract class IntEntity : IEntity<int>
    {
        public virtual int Id { get; set; }
    }
}