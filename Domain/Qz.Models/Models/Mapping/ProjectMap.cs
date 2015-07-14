using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProjectNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ProjectName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Area)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.DevelopEnterprise)
                .HasMaxLength(1000);

            this.Property(t => t.PreSaleXuNo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Projects");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProjectNo).HasColumnName("ProjectNo");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.DevelopEnterprise).HasColumnName("DevelopEnterprise");
            this.Property(t => t.ApproveTime).HasColumnName("ApproveTime");
            this.Property(t => t.PreSaleXuNo).HasColumnName("PreSaleXuNo");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
