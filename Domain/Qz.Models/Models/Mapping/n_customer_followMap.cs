using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class n_customer_followMap : EntityTypeConfiguration<n_customer_follow>
    {
        public n_customer_followMap()
        {
            // Primary Key
            this.HasKey(t => t.follow_id);

            // Properties
            this.Property(t => t.follow_username)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.follow_deptname)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.follow_content)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("n_customer_follow");
            this.Property(t => t.follow_id).HasColumnName("follow_id");
            this.Property(t => t.customer_id).HasColumnName("customer_id");
            this.Property(t => t.follow_userid).HasColumnName("follow_userid");
            this.Property(t => t.follow_username).HasColumnName("follow_username");
            this.Property(t => t.follow_deptid).HasColumnName("follow_deptid");
            this.Property(t => t.follow_deptname).HasColumnName("follow_deptname");
            this.Property(t => t.follow_type).HasColumnName("follow_type");
            this.Property(t => t.follow_status).HasColumnName("follow_status");
            this.Property(t => t.follow_content).HasColumnName("follow_content");
            this.Property(t => t.follow_dltflag).HasColumnName("follow_dltflag");
            this.Property(t => t.follow_date).HasColumnName("follow_date");
        }
    }
}
