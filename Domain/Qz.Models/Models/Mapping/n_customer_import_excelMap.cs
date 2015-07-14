using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Qz.Models.Mapping
{
    public class n_customer_import_excelMap : EntityTypeConfiguration<n_customer_import_excel>
    {
        public n_customer_import_excelMap()
        {
            // Primary Key
            this.HasKey(t => t.import_excel_id);

            // Properties
            this.Property(t => t.import_batch)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

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

            this.Property(t => t.import_result_msg)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("n_customer_import_excel");
            this.Property(t => t.import_excel_id).HasColumnName("import_excel_id");
            this.Property(t => t.import_batch).HasColumnName("import_batch");
            this.Property(t => t.customer_name).HasColumnName("customer_name");
            this.Property(t => t.customer_tel).HasColumnName("customer_tel");
            this.Property(t => t.customer_tel_append).HasColumnName("customer_tel_append");
            this.Property(t => t.customer_remarks).HasColumnName("customer_remarks");
            this.Property(t => t.impotr_result).HasColumnName("impotr_result");
            this.Property(t => t.import_result_msg).HasColumnName("import_result_msg");
        }
    }
}
