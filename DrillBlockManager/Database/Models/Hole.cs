using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrillBlockManager.Database.Models
{
    public class Hole
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public int Depth { get; set; }

        #region Association
            
        public IEnumerable<HolePoint> HolePoints { get; init; }

        #endregion
    }

    public class HoleConfiguration : IEntityTypeConfiguration<Hole>
    {
        public void Configure(EntityTypeBuilder<Hole> builder)
        {
            builder.ToTable("holes", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name");

            builder.Property(x => x.Depth)
                .HasColumnName("hole_depth");
        }
    }
}
