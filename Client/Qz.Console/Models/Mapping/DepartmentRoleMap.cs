using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Console.Models.Mapping
{
    public class DepartmentRoleMap : EntityTypeConfiguration<DepartmentRole>
    {
        public DepartmentRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("DepartmentRoles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");

            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.DepartmentRoles)
                .HasForeignKey(d => d.DepartmentId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.DepartmentRoles)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
