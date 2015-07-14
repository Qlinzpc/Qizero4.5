using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class n_customer_washMap : EntityTypeConfiguration<n_customer_wash>
    {
        public n_customer_washMap()
        {
            // Primary Key
            this.HasKey(t => new { t.customer_id, t.customer_name, t.customer_tel, t.customer_category, t.customer_source, t.customer_level, t.customer_paytype, t.customer_isself, t.customer_dltflag, t.WashUserId, t.DataStatus, t.DltFlag });

            // Properties
            this.Property(t => t.customer_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.customer_name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.customer_tel)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.customer_tel_append)
                .HasMaxLength(50);

            this.Property(t => t.customer_category)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.customer_source)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.customer_remarks)
                .HasMaxLength(500);

            this.Property(t => t.customer_level)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.customer_paytype)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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

            this.Property(t => t.customer_isself)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.customer_dltflag)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.owner_username)
                .HasMaxLength(20);

            this.Property(t => t.owner_deptname)
                .HasMaxLength(20);

            this.Property(t => t.WashUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DataStatus)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DltFlag)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("n_customer_wash");
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
            this.Property(t => t.WashDate).HasColumnName("WashDate");
            this.Property(t => t.WashUserId).HasColumnName("WashUserId");
            this.Property(t => t.DataStatus).HasColumnName("DataStatus");
            this.Property(t => t.DltFlag).HasColumnName("DltFlag");
        }
    }
}
