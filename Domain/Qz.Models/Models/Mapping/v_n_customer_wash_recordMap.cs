using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class v_n_customer_wash_recordMap : EntityTypeConfiguration<v_n_customer_wash_record>
    {
        public v_n_customer_wash_recordMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.CustomerId, t.CustomerName, t.CustomerCategory, t.CustomerSource, t.CustomerLevel, t.WashUserId, t.WashDeptId, t.WashTag });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CustomerCategory)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerSource)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerLevel)
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

            // Table & Column Mappings
            this.ToTable("v_n_customer_wash_record");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.CustomerCategory).HasColumnName("CustomerCategory");
            this.Property(t => t.CustomerSource).HasColumnName("CustomerSource");
            this.Property(t => t.CustomerLevel).HasColumnName("CustomerLevel");
            this.Property(t => t.WashUserId).HasColumnName("WashUserId");
            this.Property(t => t.WashUserName).HasColumnName("WashUserName");
            this.Property(t => t.WashDeptId).HasColumnName("WashDeptId");
            this.Property(t => t.WashDeptName).HasColumnName("WashDeptName");
            this.Property(t => t.WashDate).HasColumnName("WashDate");
            this.Property(t => t.WashTag).HasColumnName("WashTag");
        }
    }
}
