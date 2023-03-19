using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrillBlockManager.Database.Models
{
    public class HolePoint
    {
        public Guid Id { get; init; }
        public Guid HoleId { get; init; }
        public Coordinate Coordinate { get; init; }

        #region Association

        public Hole Hole { get; init; }

        #endregion
    }

    public class HolePointConfiguration : IEntityTypeConfiguration<HolePoint>
    {
        public void Configure(EntityTypeBuilder<HolePoint> builder)
        {
            builder.ToTable("holes_points", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.HoleId)
                .HasColumnName("hole_id");

            builder.Property(x => x.Coordinate)
                .HasColumnName("coordinate")
                .HasColumnType("jsonb");

            builder.HasOne(x => x.Hole)
                .WithMany(x => x.HolePoints)
                .HasForeignKey(x => x.HoleId);
        }
    }
}
