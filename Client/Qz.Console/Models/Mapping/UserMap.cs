using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Console.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(255);

            this.Property(t => t.Account)
                .HasMaxLength(255);

            this.Property(t => t.Password)
                .HasMaxLength(255);

            this.Property(t => t.Secretkey)
                .HasMaxLength(255);

            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.Spell)
                .HasMaxLength(50);

            this.Property(t => t.Gender)
                .HasMaxLength(5);

            this.Property(t => t.Birthday)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.Config)
                .HasMaxLength(1000);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            this.Property(t => t.CreateUserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModifyUserName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.InnerUser).HasColumnName("InnerUser");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Account).HasColumnName("Account");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Secretkey).HasColumnName("Secretkey");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Spell).HasColumnName("Spell");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.ChangePasswordDate).HasColumnName("ChangePasswordDate");
            this.Property(t => t.OpenId).HasColumnName("OpenId");
            this.Property(t => t.LoginCount).HasColumnName("LoginCount");
            this.Property(t => t.FirstVisit).HasColumnName("FirstVisit");
            this.Property(t => t.PreviousVisit).HasColumnName("PreviousVisit");
            this.Property(t => t.LastVisit).HasColumnName("LastVisit");
            this.Property(t => t.Online).HasColumnName("Online");
            this.Property(t => t.Config).HasColumnName("Config");
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

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.CompanyId);
            this.HasRequired(t => t.Department)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.DepartmentId);

        }
    }
}
