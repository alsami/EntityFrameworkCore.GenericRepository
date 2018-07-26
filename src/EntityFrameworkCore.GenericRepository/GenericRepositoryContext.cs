using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.GenericRepository
{
    public abstract class GenericRepositoryContext : DbContext
    {
        protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
    }
}