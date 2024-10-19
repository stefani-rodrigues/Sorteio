using Microsoft.EntityFrameworkCore;

namespace sorteio.Repositorio.Contexto
{
    public class ContextoBanco : DbContext
    {

        public ContextoBanco(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoBanco).Assembly);
            modelBuilder.Ignore<Dictionary<string, object>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(); // Habilita o Lazy Loading
            base.OnConfiguring(optionsBuilder);
        }
    }
}
