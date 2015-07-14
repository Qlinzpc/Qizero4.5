using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class ProjectDetailMap : EntityTypeConfiguration<ProjectDetail>
    {
        public ProjectDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProjectNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.DurableYears)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.HousingUse)
                .HasMaxLength(50);

            this.Property(t => t.LandUse)
                .HasMaxLength(50);

            this.Property(t => t.SalePhone1)
                .HasMaxLength(20);

            this.Property(t => t.SalePhone2)
                .HasMaxLength(20);

            this.Property(t => t.PropertyManagementCompany)
                .HasMaxLength(500);

            this.Property(t => t.ManagementCost)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ProjectDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProjectNo).HasColumnName("ProjectNo");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.TransfereeDate).HasColumnName("TransfereeDate");
            this.Property(t => t.DurableYears).HasColumnName("DurableYears");
            this.Property(t => t.HousingUse).HasColumnName("HousingUse");
            this.Property(t => t.LandUse).HasColumnName("LandUse");
            this.Property(t => t.LandArea).HasColumnName("LandArea");
            this.Property(t => t.OverallFloorage).HasColumnName("OverallFloorage");
            this.Property(t => t.PreSaleTotal).HasColumnName("PreSaleTotal");
            this.Property(t => t.PreSaleArea).HasColumnName("PreSaleArea");
            this.Property(t => t.SalePhone1).HasColumnName("SalePhone1");
            this.Property(t => t.SalePhone2).HasColumnName("SalePhone2");
            this.Property(t => t.PropertyManagementCompany).HasColumnName("PropertyManagementCompany");
            this.Property(t => t.ManagementCost).HasColumnName("ManagementCost");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
