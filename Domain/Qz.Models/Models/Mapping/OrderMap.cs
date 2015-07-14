using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Orderid);

            // Properties
            this.Property(t => t.Orderid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("OrderS");
            this.Property(t => t.Orderid).HasColumnName("Orderid");
            this.Property(t => t.custid).HasColumnName("custid");
        }
    }
}
