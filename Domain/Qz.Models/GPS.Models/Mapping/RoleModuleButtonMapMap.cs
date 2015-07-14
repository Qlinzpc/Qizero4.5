using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class RoleModuleButtonMapMap : EntityTypeConfiguration<RoleModuleButtonMap>
    {
        public RoleModuleButtonMapMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RoleModuleButtonMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ButtonId).HasColumnName("ButtonId");

            // Relationships
            this.HasRequired(t => t.Button);
                //.WithMany(t => t.RoleModuleButtonMaps)
                //.HasForeignKey(d => d.ButtonId);
            this.HasRequired(t => t.Module);
                //.WithMany(t => t.RoleModuleButtonMaps)
                //.HasForeignKey(d => d.ModuleId);
            this.HasRequired(t => t.Role);
                //.WithMany(t => t.RoleModuleButtonMaps)
                //.HasForeignKey(d => d.RoleId);

        }
    }
}
