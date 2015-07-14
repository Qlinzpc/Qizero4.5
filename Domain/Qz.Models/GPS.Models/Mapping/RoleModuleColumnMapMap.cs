using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class RoleModuleColumnMapMap : EntityTypeConfiguration<RoleModuleColumnMap>
    {
        public RoleModuleColumnMapMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FieleName)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("RoleModuleColumnMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.IsReject).HasColumnName("IsReject");
            this.Property(t => t.FieleName).HasColumnName("FieleName");

            // Relationships
            this.HasRequired(t => t.Module);
                //.WithMany(t => t.RoleModuleColumnMaps)
                //.HasForeignKey(d => d.ModuleId);
            this.HasRequired(t => t.Role);
                //.WithMany(t => t.RoleModuleColumnMaps)
                //.HasForeignKey(d => d.RoleId);

        }
    }
}
