using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Console.Models.Mapping
{
    public class RoleModuleMapMap : EntityTypeConfiguration<RoleModuleMap>
    {
        public RoleModuleMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleId, t.ModuleId });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.RoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModuleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RoleModuleMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");

            // Relationships
            this.HasRequired(t => t.Module)
                .WithMany(t => t.RoleModuleMaps)
                .HasForeignKey(d => d.ModuleId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RoleModuleMaps)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
