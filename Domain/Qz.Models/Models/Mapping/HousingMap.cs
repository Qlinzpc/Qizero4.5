using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class HousingMap : EntityTypeConfiguration<Housing>
    {
        public HousingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DName)
                .HasMaxLength(20);

            this.Property(t => t.Area)
                .HasMaxLength(20);

            this.Property(t => t.BuildName)
                .HasMaxLength(150);

            this.Property(t => t.EstateName)
                .HasMaxLength(100);

            this.Property(t => t.RoomNo)
                .HasMaxLength(100);

            this.Property(t => t.Owner)
                .HasMaxLength(60);

            this.Property(t => t.OwnerTel)
                .HasMaxLength(100);

            this.Property(t => t.PocNo)
                .HasMaxLength(100);

            this.Property(t => t.PocStatus)
                .HasMaxLength(10);

            this.Property(t => t.UseStatus)
                .HasMaxLength(10);

            this.Property(t => t.PropertyRight)
                .HasMaxLength(10);

            this.Property(t => t.Fitment)
                .HasMaxLength(10);

            this.Property(t => t.HouseStyle)
                .HasMaxLength(10);

            this.Property(t => t.MergePy)
                .HasMaxLength(100);

            this.Property(t => t.AddUser)
                .HasMaxLength(50);

            this.Property(t => t.AddUDept)
                .HasMaxLength(50);

            this.Property(t => t.MarkUDept)
                .HasMaxLength(50);

            this.Property(t => t.MarkUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Housing");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DId).HasColumnName("DId");
            this.Property(t => t.DName).HasColumnName("DName");
            this.Property(t => t.AId).HasColumnName("AId");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.BuildId).HasColumnName("BuildId");
            this.Property(t => t.BuildName).HasColumnName("BuildName");
            this.Property(t => t.EId).HasColumnName("EId");
            this.Property(t => t.EstateName).HasColumnName("EstateName");
            this.Property(t => t.RoomId).HasColumnName("RoomId");
            this.Property(t => t.RoomNo).HasColumnName("RoomNo");
            this.Property(t => t.TradeType).HasColumnName("TradeType");
            this.Property(t => t.TotalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.LeasePrice).HasColumnName("LeasePrice");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Owner).HasColumnName("Owner");
            this.Property(t => t.OwnerTel).HasColumnName("OwnerTel");
            this.Property(t => t.CostPrice).HasColumnName("CostPrice");
            this.Property(t => t.PocNo).HasColumnName("PocNo");
            this.Property(t => t.PocStatus).HasColumnName("PocStatus");
            this.Property(t => t.Redeem).HasColumnName("Redeem");
            this.Property(t => t.UseStatus).HasColumnName("UseStatus");
            this.Property(t => t.PropertyRight).HasColumnName("PropertyRight");
            this.Property(t => t.Fitment).HasColumnName("Fitment");
            this.Property(t => t.HouseStyle).HasColumnName("HouseStyle");
            this.Property(t => t.Use).HasColumnName("Use");
            this.Property(t => t.MergePy).HasColumnName("MergePy");
            this.Property(t => t.MarkDate).HasColumnName("MarkDate");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.AddUId).HasColumnName("AddUId");
            this.Property(t => t.AddUser).HasColumnName("AddUser");
            this.Property(t => t.AddUDept).HasColumnName("AddUDept");
            this.Property(t => t.AddUDeptId).HasColumnName("AddUDeptId");
            this.Property(t => t.MarkUDept).HasColumnName("MarkUDept");
            this.Property(t => t.UDDate).HasColumnName("UDDate");
            this.Property(t => t.MarkUId).HasColumnName("MarkUId");
            this.Property(t => t.MarkUser).HasColumnName("MarkUser");
            this.Property(t => t.DelFalg).HasColumnName("DelFalg");
        }
    }
}
