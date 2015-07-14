using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class n_customer_wash_tag_totalMap : EntityTypeConfiguration<n_customer_wash_tag_total>
    {
        public n_customer_wash_tag_totalMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.CustomerId, t.NPowerOff, t.NOccupy, t.NStop, t.NMissedCall, t.NRestrictedPhone, t.NInvalidPhone, t.NOutOfReach, t.NOther, t.YToSelf, t.YLeasedSold, t.YNotRentSell, t.YPeers, t.YNotOneself, t.YHangUp, t.YOther });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NPowerOff)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOccupy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NStop)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NMissedCall)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NRestrictedPhone)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NInvalidPhone)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOutOfReach)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOther)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.YToSelf)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.YLeasedSold)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.YNotRentSell)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.YPeers)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.YNotOneself)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.YHangUp)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.YOther)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("n_customer_wash_tag_total");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.NPowerOff).HasColumnName("NPowerOff");
            this.Property(t => t.NOccupy).HasColumnName("NOccupy");
            this.Property(t => t.NStop).HasColumnName("NStop");
            this.Property(t => t.NMissedCall).HasColumnName("NMissedCall");
            this.Property(t => t.NRestrictedPhone).HasColumnName("NRestrictedPhone");
            this.Property(t => t.NInvalidPhone).HasColumnName("NInvalidPhone");
            this.Property(t => t.NOutOfReach).HasColumnName("NOutOfReach");
            this.Property(t => t.NOther).HasColumnName("NOther");
            this.Property(t => t.YToSelf).HasColumnName("YToSelf");
            this.Property(t => t.YLeasedSold).HasColumnName("YLeasedSold");
            this.Property(t => t.YNotRentSell).HasColumnName("YNotRentSell");
            this.Property(t => t.YPeers).HasColumnName("YPeers");
            this.Property(t => t.YNotOneself).HasColumnName("YNotOneself");
            this.Property(t => t.YHangUp).HasColumnName("YHangUp");
            this.Property(t => t.YOther).HasColumnName("YOther");
        }
    }
}
