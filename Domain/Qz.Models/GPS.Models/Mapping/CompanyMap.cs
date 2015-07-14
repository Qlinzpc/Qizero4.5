using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.Nature)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Manager)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Contact)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            this.Property(t => t.CreateUserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModifyUserName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Company");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.Nature).HasColumnName("Nature");
            this.Property(t => t.Manager).HasColumnName("Manager");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Enabled).HasColumnName("Enabled");
            this.Property(t => t.SortCode).HasColumnName("SortCode");
            this.Property(t => t.IsDelete).HasColumnName("IsDelete");
            this.Property(t => t.CreateUserId).HasColumnName("CreateUserId");
            this.Property(t => t.CreateUserName).HasColumnName("CreateUserName");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyUserId).HasColumnName("ModifyUserId");
            this.Property(t => t.ModifyUserName).HasColumnName("ModifyUserName");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
        }
    }
}
