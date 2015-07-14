using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Console.Models.Mapping
{
    public class RoleModuleButtonMapMap : EntityTypeConfiguration<RoleModuleButtonMap>
    {
        public RoleModuleButtonMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleId, t.ModuleId, t.ButtonId });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.RoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModuleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ButtonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RoleModuleButtonMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ButtonId).HasColumnName("ButtonId");

            // Relationships
            this.HasRequired(t => t.Button)
                .WithMany(t => t.RoleModuleButtonMaps)
                .HasForeignKey(d => d.ButtonId);
            this.HasRequired(t => t.Module)
                .WithMany(t => t.RoleModuleButtonMaps)
                .HasForeignKey(d => d.ModuleId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RoleModuleButtonMaps)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
