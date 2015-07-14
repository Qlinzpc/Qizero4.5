using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class n_customer_importMap : EntityTypeConfiguration<n_customer_import>
    {
        public n_customer_importMap()
        {
            // Primary Key
            this.HasKey(t => t.import_batch);

            // Properties
            this.Property(t => t.import_batch)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.import_name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.owner_deptname)
                .HasMaxLength(20);

            this.Property(t => t.add_username)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.add_deptname)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("n_customer_import");
            this.Property(t => t.import_batch).HasColumnName("import_batch");
            this.Property(t => t.import_name).HasColumnName("import_name");
            this.Property(t => t.customer_category).HasColumnName("customer_category");
            this.Property(t => t.owner_deptid).HasColumnName("owner_deptid");
            this.Property(t => t.owner_deptname).HasColumnName("owner_deptname");
            this.Property(t => t.owner_date).HasColumnName("owner_date");
            this.Property(t => t.add_userid).HasColumnName("add_userid");
            this.Property(t => t.add_username).HasColumnName("add_username");
            this.Property(t => t.add_deptid).HasColumnName("add_deptid");
            this.Property(t => t.add_deptname).HasColumnName("add_deptname");
            this.Property(t => t.add_date).HasColumnName("add_date");
        }
    }
}
