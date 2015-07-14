using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class n_customerMap : EntityTypeConfiguration<n_customer>
    {
        public n_customerMap()
        {
            // Primary Key
            this.HasKey(t => t.customer_id);

            // Properties
            this.Property(t => t.customer_name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.customer_tel)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.customer_tel_append)
                .HasMaxLength(50);

            this.Property(t => t.customer_remarks)
                .HasMaxLength(500);

            this.Property(t => t.city_ids)
                .HasMaxLength(200);

            this.Property(t => t.city_names)
                .HasMaxLength(200);

            this.Property(t => t.area_ids)
                .HasMaxLength(200);

            this.Property(t => t.area_names)
                .HasMaxLength(200);

            this.Property(t => t.estate_ids)
                .HasMaxLength(200);

            this.Property(t => t.estate_names)
                .HasMaxLength(200);

            this.Property(t => t.owner_username)
                .HasMaxLength(20);

            this.Property(t => t.owner_deptname)
                .HasMaxLength(20);

            this.Property(t => t.add_username)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.add_deptname)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.modify_username)
                .HasMaxLength(20);

            this.Property(t => t.modify_deptname)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("n_customer");
            this.Property(t => t.customer_id).HasColumnName("customer_id");
            this.Property(t => t.customer_name).HasColumnName("customer_name");
            this.Property(t => t.customer_tel).HasColumnName("customer_tel");
            this.Property(t => t.customer_tel_append).HasColumnName("customer_tel_append");
            this.Property(t => t.customer_category).HasColumnName("customer_category");
            this.Property(t => t.customer_source).HasColumnName("customer_source");
            this.Property(t => t.customer_remarks).HasColumnName("customer_remarks");
            this.Property(t => t.customer_level).HasColumnName("customer_level");
            this.Property(t => t.customer_paytype).HasColumnName("customer_paytype");
            this.Property(t => t.city_ids).HasColumnName("city_ids");
            this.Property(t => t.city_names).HasColumnName("city_names");
            this.Property(t => t.area_ids).HasColumnName("area_ids");
            this.Property(t => t.area_names).HasColumnName("area_names");
            this.Property(t => t.estate_ids).HasColumnName("estate_ids");
            this.Property(t => t.estate_names).HasColumnName("estate_names");
            this.Property(t => t.price).HasColumnName("price");
            this.Property(t => t.acreage).HasColumnName("acreage");
            this.Property(t => t.room).HasColumnName("room");
            this.Property(t => t.customer_isself).HasColumnName("customer_isself");
            this.Property(t => t.customer_dltflag).HasColumnName("customer_dltflag");
            this.Property(t => t.follow_newstatus).HasColumnName("follow_newstatus");
            this.Property(t => t.follow_newsdate).HasColumnName("follow_newsdate");
            this.Property(t => t.seehousing_count).HasColumnName("seehousing_count");
            this.Property(t => t.owner_userid).HasColumnName("owner_userid");
            this.Property(t => t.owner_username).HasColumnName("owner_username");
            this.Property(t => t.owner_deptid).HasColumnName("owner_deptid");
            this.Property(t => t.owner_deptname).HasColumnName("owner_deptname");
            this.Property(t => t.owner_date).HasColumnName("owner_date");
            this.Property(t => t.add_userid).HasColumnName("add_userid");
            this.Property(t => t.add_username).HasColumnName("add_username");
            this.Property(t => t.add_deptid).HasColumnName("add_deptid");
            this.Property(t => t.add_deptname).HasColumnName("add_deptname");
            this.Property(t => t.add_date).HasColumnName("add_date");
            this.Property(t => t.modify_userid).HasColumnName("modify_userid");
            this.Property(t => t.modify_username).HasColumnName("modify_username");
            this.Property(t => t.modify_deptid).HasColumnName("modify_deptid");
            this.Property(t => t.modify_deptname).HasColumnName("modify_deptname");
            this.Property(t => t.modify_date).HasColumnName("modify_date");
        }
    }
}
