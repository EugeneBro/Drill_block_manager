using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrillBlockManager.Database.Models
{
    public class DrillBlockPoint
    {
        public Guid Id { get; init; }
        public int PositionInSequence { get; init; }
        public Coordinate Coordinate { get; init; }
        public Guid DrillBlockId { get; init; }

        #region Association

        public DrillBlock DrillBlock { get; init; }

        #endregion
    }

    public class DrillBlockPointConfiguration : IEntityTypeConfiguration<DrillBlockPoint>
    {
        public void Configure(EntityTypeBuilder<DrillBlockPoint> builder)
        {
            builder.ToTable("drill_blocks_points", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.PositionInSequence)
                .HasColumnName("position_in_sequence");

            builder.Property(x => x.Coordinate)
                .HasColumnName("coordinate")
                .HasColumnType("jsonb");

            builder.Property(x => x.DrillBlockId)
                .HasColumnName("drill_block_id");

            builder.HasOne(x => x.DrillBlock)
                .WithMany(x => x.DrillBlockPoints)
                .HasForeignKey(x => x.DrillBlockId);
        }
    }
}
