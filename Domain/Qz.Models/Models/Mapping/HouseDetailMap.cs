using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class HouseDetailMap : EntityTypeConfiguration<HouseDetail>
    {
        public HouseDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.HouseNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.HouseType)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.DevelopersOutcry)
                .HasMaxLength(20);

            this.Property(t => t.HouseUse)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CoveredArea)
                .HasMaxLength(20);

            this.Property(t => t.IndoorArea)
                .HasMaxLength(20);

            this.Property(t => t.AssessmentArea)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("HouseDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HouseNo).HasColumnName("HouseNo");
            this.Property(t => t.HouseType).HasColumnName("HouseType");
            this.Property(t => t.DevelopersOutcry).HasColumnName("DevelopersOutcry");
            this.Property(t => t.HouseUse).HasColumnName("HouseUse");
            this.Property(t => t.CoveredArea).HasColumnName("CoveredArea");
            this.Property(t => t.IndoorArea).HasColumnName("IndoorArea");
            this.Property(t => t.AssessmentArea).HasColumnName("AssessmentArea");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
