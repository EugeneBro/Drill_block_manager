using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrillBlockManager.Database.Models
{
    public class DrillBlock
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }

        #region Association

        public IEnumerable<DrillBlockPoint> DrillBlockPoints { get; init; }

        #endregion
    }

    public class DrillBlockConfiguration : IEntityTypeConfiguration<DrillBlock>
    {
        public void Configure(EntityTypeBuilder<DrillBlock> builder)
        {
            builder.ToTable("drill_blocks", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name");

            builder.Property(x => x.UpdateDate)
                .HasColumnName("update_date");
        }
    }
}
