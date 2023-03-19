using DrillBlockManager.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DrillBlockManager.Database
{
    public class PostgreDbContext : DbContext
    {
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
        {
        }

        #region Tables
        
        public DbSet<DrillBlock> DrillBlocks { get; init; }
        public DbSet<DrillBlockPoint> DrillBlockPoints { get; init; }
        public DbSet<Hole> Holes { get; init; }
        public DbSet<HolePoint> HolePoints { get; init; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
