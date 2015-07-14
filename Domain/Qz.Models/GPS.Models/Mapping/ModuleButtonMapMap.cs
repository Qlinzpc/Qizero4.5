using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class ModuleButtonMapMap : EntityTypeConfiguration<ModuleButtonMap>
    {
        public ModuleButtonMapMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ModuleButtonMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ButtonId).HasColumnName("ButtonId");

            // Relationships
            this.HasRequired(t => t.Button);
                //.WithMany(t => t.ModuleButtonMaps)
                //.HasForeignKey(d => d.ButtonId);
            this.HasRequired(t => t.Module);
                //.WithMany(t => t.ModuleButtonMaps)
                //.HasForeignKey(d => d.ModuleId);

        }
    }
}
