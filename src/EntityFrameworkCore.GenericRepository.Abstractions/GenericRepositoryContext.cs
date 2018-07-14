using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.GenericRepository.Abstractions
{
    public abstract class GenericRepositoryContext : DbContext
    {
        protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
    }
}