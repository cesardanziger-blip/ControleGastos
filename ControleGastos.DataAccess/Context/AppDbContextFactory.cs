using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ControleGastos.DataAccess.Context
{
    /// <summary>
    /// Cria o DbContext em tempo de design para o EF Core Tools (migrações)
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Configura o SQLite igual ao Program.cs
            optionsBuilder.UseSqlite("Data Source=controlegastos.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}