using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Qz.Models.Mapping;

namespace Qz.Models
{
    public partial class QlinContext : DbContext
    {
        static QlinContext()
        {
            Database.SetInitializer<QlinContext>(null);
        }

        public QlinContext()
            : base("Name=QlinContext")
        {
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HouseDetail> HouseDetails { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Housing> Housings { get; set; }
        public DbSet<n_customer> n_customer { get; set; }
        public DbSet<n_customer_follow> n_customer_follow { get; set; }
        public DbSet<n_customer_import> n_customer_import { get; set; }
        public DbSet<n_customer_import_excel> n_customer_import_excel { get; set; }
        public DbSet<n_customer_wash> n_customer_wash { get; set; }
        public DbSet<n_customer_wash_record> n_customer_wash_record { get; set; }
        public DbSet<n_customer_wash_tag_total> n_customer_wash_tag_total { get; set; }
        public DbSet<NLog_Error> NLog_Error { get; set; }
        public DbSet<Order> OrderS { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<T> T { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<v_n_customer_wash_record> v_n_customer_wash_record { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BuildingMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new HouseDetailMap());
            modelBuilder.Configurations.Add(new HouseMap());
            modelBuilder.Configurations.Add(new HousingMap());
            modelBuilder.Configurations.Add(new n_customerMap());
            modelBuilder.Configurations.Add(new n_customer_followMap());
            modelBuilder.Configurations.Add(new n_customer_importMap());
            modelBuilder.Configurations.Add(new n_customer_import_excelMap());
            modelBuilder.Configurations.Add(new n_customer_washMap());
            modelBuilder.Configurations.Add(new n_customer_wash_recordMap());
            modelBuilder.Configurations.Add(new n_customer_wash_tag_totalMap());
            modelBuilder.Configurations.Add(new NLog_ErrorMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProjectDetailMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new TMap());
            modelBuilder.Configurations.Add(new TestMap());
            modelBuilder.Configurations.Add(new v_n_customer_wash_recordMap());
        }
    }
}
