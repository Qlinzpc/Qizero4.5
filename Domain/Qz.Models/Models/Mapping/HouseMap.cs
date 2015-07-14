using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class HouseMap : EntityTypeConfiguration<House>
    {
        public HouseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.HouseNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.BuildNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.HouseName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.HouseFloor)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Houses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HouseNo).HasColumnName("HouseNo");
            this.Property(t => t.BuildNo).HasColumnName("BuildNo");
            this.Property(t => t.HouseName).HasColumnName("HouseName");
            this.Property(t => t.HouseFloor).HasColumnName("HouseFloor");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
