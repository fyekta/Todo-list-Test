using App.Domain.Core.Entity;
using Microsoft.EntityFrameworkCore;


namespace App.Infra.Data.Db.SqlServer.Ef.Dbctx
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
     : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
