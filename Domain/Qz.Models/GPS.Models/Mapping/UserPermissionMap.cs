using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.GPS.Models.Mapping
{
    public class UserPermissionMap : EntityTypeConfiguration<UserPermission>
    {
        public UserPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserPermissions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.PermissionId).HasColumnName("PermissionId");

            // Relationships
            this.HasRequired(t => t.Permission);
                //.WithMany(t => t.UserPermissions)
                //.HasForeignKey(d => d.PermissionId);
            this.HasRequired(t => t.User);
                //.WithMany(t => t.UserPermissions)
                //.HasForeignKey(d => d.UserId);

        }
    }
}
