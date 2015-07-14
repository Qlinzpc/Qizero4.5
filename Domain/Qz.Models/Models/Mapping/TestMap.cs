using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class TestMap : EntityTypeConfiguration<Test>
    {
        public TestMap()
        {
            // Primary Key
            this.HasKey(t => t.RowNumber);

            // Properties
            this.Property(t => t.ApplicationName)
                .HasMaxLength(128);

            this.Property(t => t.DatabaseName)
                .HasMaxLength(128);

            this.Property(t => t.HostName)
                .HasMaxLength(128);

            this.Property(t => t.LoginName)
                .HasMaxLength(128);

            this.Property(t => t.NTDomainName)
                .HasMaxLength(128);

            this.Property(t => t.NTUserName)
                .HasMaxLength(128);

            this.Property(t => t.ServerName)
                .HasMaxLength(128);

            this.Property(t => t.SessionLoginName)
                .HasMaxLength(128);

            this.Property(t => t.ObjectName)
                .HasMaxLength(128);

            this.Property(t => t.MethodName)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Test");
            this.Property(t => t.RowNumber).HasColumnName("RowNumber");
            this.Property(t => t.EventClass).HasColumnName("EventClass");
            this.Property(t => t.CPU).HasColumnName("CPU");
            this.Property(t => t.SPID).HasColumnName("SPID");
            this.Property(t => t.ApplicationName).HasColumnName("ApplicationName");
            this.Property(t => t.ClientProcessID).HasColumnName("ClientProcessID");
            this.Property(t => t.DatabaseID).HasColumnName("DatabaseID");
            this.Property(t => t.DatabaseName).HasColumnName("DatabaseName");
            this.Property(t => t.EventSequence).HasColumnName("EventSequence");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.Handle).HasColumnName("Handle");
            this.Property(t => t.HostName).HasColumnName("HostName");
            this.Property(t => t.IsSystem).HasColumnName("IsSystem");
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.LoginSid).HasColumnName("LoginSid");
            this.Property(t => t.NTDomainName).HasColumnName("NTDomainName");
            this.Property(t => t.NTUserName).HasColumnName("NTUserName");
            this.Property(t => t.RequestID).HasColumnName("RequestID");
            this.Property(t => t.ServerName).HasColumnName("ServerName");
            this.Property(t => t.SessionLoginName).HasColumnName("SessionLoginName");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.XactSequence).HasColumnName("XactSequence");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Error).HasColumnName("Error");
            this.Property(t => t.Reads).HasColumnName("Reads");
            this.Property(t => t.RowCounts).HasColumnName("RowCounts");
            this.Property(t => t.TextData).HasColumnName("TextData");
            this.Property(t => t.Writes).HasColumnName("Writes");
            this.Property(t => t.IntegerData).HasColumnName("IntegerData");
            this.Property(t => t.IntegerData2).HasColumnName("IntegerData2");
            this.Property(t => t.LineNumber).HasColumnName("LineNumber");
            this.Property(t => t.NestLevel).HasColumnName("NestLevel");
            this.Property(t => t.Offset).HasColumnName("Offset");
            this.Property(t => t.EventSubClass).HasColumnName("EventSubClass");
            this.Property(t => t.ObjectID).HasColumnName("ObjectID");
            this.Property(t => t.ObjectName).HasColumnName("ObjectName");
            this.Property(t => t.ObjectType).HasColumnName("ObjectType");
            this.Property(t => t.SqlHandle).HasColumnName("SqlHandle");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.MethodName).HasColumnName("MethodName");
            this.Property(t => t.BinaryData).HasColumnName("BinaryData");
        }
    }
}
