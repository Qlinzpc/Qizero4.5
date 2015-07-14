using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class NLog_ErrorMap : EntityTypeConfiguration<NLog_Error>
    {
        public NLog_ErrorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.host)
                .IsRequired();

            this.Property(t => t.type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.source)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.message)
                .IsRequired();

            this.Property(t => t.level)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.logger)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.stacktrace)
                .IsRequired();

            this.Property(t => t.allxml)
                .IsRequired();

            this.Property(t => t.detail)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("NLog_Error");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.time_stamp).HasColumnName("time_stamp");
            this.Property(t => t.host).HasColumnName("host");
            this.Property(t => t.type).HasColumnName("type");
            this.Property(t => t.source).HasColumnName("source");
            this.Property(t => t.message).HasColumnName("message");
            this.Property(t => t.level).HasColumnName("level");
            this.Property(t => t.logger).HasColumnName("logger");
            this.Property(t => t.stacktrace).HasColumnName("stacktrace");
            this.Property(t => t.allxml).HasColumnName("allxml");
            this.Property(t => t.detail).HasColumnName("detail");
        }
    }
}
