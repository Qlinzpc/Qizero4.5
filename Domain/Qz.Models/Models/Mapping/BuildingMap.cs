using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class BuildingMap : EntityTypeConfiguration<Building>
    {
        public BuildingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.BuildNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ProjectNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.BuildName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ParentNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.PlanningLicence)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ConstrucLicence)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Buildings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BuildNo).HasColumnName("BuildNo");
            this.Property(t => t.ProjectNo).HasColumnName("ProjectNo");
            this.Property(t => t.BuildName).HasColumnName("BuildName");
            this.Property(t => t.ParentNo).HasColumnName("ParentNo");
            this.Property(t => t.PlanningLicence).HasColumnName("PlanningLicence");
            this.Property(t => t.ConstrucLicence).HasColumnName("ConstrucLicence");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
