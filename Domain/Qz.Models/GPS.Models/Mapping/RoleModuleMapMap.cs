using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class RoleModuleMapMap : EntityTypeConfiguration<RoleModuleMap>
    {
        public RoleModuleMapMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RoleModuleMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");

            // Relationships
            this.HasRequired(t => t.Module);
                //.WithMany(t => t.RoleModuleMaps)
                //.HasForeignKey(d => d.ModuleId);
            this.HasRequired(t => t.Role);
                //.WithMany(t => t.RoleModuleMaps)
                //.HasForeignKey(d => d.RoleId);

        }
    }
}
