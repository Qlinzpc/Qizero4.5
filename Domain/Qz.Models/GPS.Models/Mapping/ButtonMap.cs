using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class ButtonMap : EntityTypeConfiguration<Button>
    {
        public ButtonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Icon)
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
            this.ToTable("Buttons");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Icon).HasColumnName("Icon");
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
