using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class LoginLogMap : EntityTypeConfiguration<LoginLog>
    {
        public LoginLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.HostName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.HostIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LoginMsg)
                .IsRequired()
                .HasMaxLength(8000);

            // Table & Column Mappings
            this.ToTable("LoginLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.HostName).HasColumnName("HostName");
            this.Property(t => t.HostIP).HasColumnName("HostIP");
            this.Property(t => t.LoginMsg).HasColumnName("LoginMsg");
            this.Property(t => t.LoginDate).HasColumnName("LoginDate");
        }
    }
}
