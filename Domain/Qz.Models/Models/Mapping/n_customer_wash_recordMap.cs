using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class n_customer_wash_recordMap : EntityTypeConfiguration<n_customer_wash_record>
    {
        public n_customer_wash_recordMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.CustomerId, t.WashUserId, t.WashDeptId, t.WashTag, t.DltFlag });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WashUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WashUserName)
                .HasMaxLength(30);

            this.Property(t => t.WashDeptId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WashDeptName)
                .HasMaxLength(30);

            this.Property(t => t.WashTag)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DltFlag)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("n_customer_wash_record");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.WashDate).HasColumnName("WashDate");
            this.Property(t => t.WashUserId).HasColumnName("WashUserId");
            this.Property(t => t.WashUserName).HasColumnName("WashUserName");
            this.Property(t => t.WashDeptId).HasColumnName("WashDeptId");
            this.Property(t => t.WashDeptName).HasColumnName("WashDeptName");
            this.Property(t => t.WashTag).HasColumnName("WashTag");
            this.Property(t => t.DltFlag).HasColumnName("DltFlag");
        }
    }
}
