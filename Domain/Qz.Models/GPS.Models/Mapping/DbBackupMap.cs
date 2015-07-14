using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class DbBackupMap : EntityTypeConfiguration<DbBackup>
    {
        public DbBackupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ServerName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DbName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.JobName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Mode)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.FilePath)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            this.Property(t => t.CreateUserName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DbBackup");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ServerName).HasColumnName("ServerName");
            this.Property(t => t.DbName).HasColumnName("DbName");
            this.Property(t => t.JobName).HasColumnName("JobName");
            this.Property(t => t.Mode).HasColumnName("Mode");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.FilePath).HasColumnName("FilePath");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Enabled).HasColumnName("Enabled");
            this.Property(t => t.CreateUserId).HasColumnName("CreateUserId");
            this.Property(t => t.CreateUserName).HasColumnName("CreateUserName");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
