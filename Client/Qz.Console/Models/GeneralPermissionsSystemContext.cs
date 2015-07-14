using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Qz.Console.Models.Mapping;

namespace Qz.Console.Models
{
    public partial class GeneralPermissionsSystemContext : DbContext
    {
        static GeneralPermissionsSystemContext()
        {
            Database.SetInitializer<GeneralPermissionsSystemContext>(null);
        }

        public GeneralPermissionsSystemContext()
            : base("Name=GeneralPermissionsSystemContext")
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Button> Buttons { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<DataCode> DataCodes { get; set; }
        public DbSet<DataCodeType> DataCodeTypes { get; set; }
        public DbSet<DbBackup> DbBackups { get; set; }
        public DbSet<DepartmentRole> DepartmentRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<ModuleButtonMap> ModuleButtonMaps { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleModuleButtonMap> RoleModuleButtonMaps { get; set; }
        public DbSet<RoleModuleColumnMap> RoleModuleColumnMaps { get; set; }
        public DbSet<RoleModuleMap> RoleModuleMaps { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<SysLog> SysLogs { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationMap());
            modelBuilder.Configurations.Add(new ButtonMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new DataCodeMap());
            modelBuilder.Configurations.Add(new DataCodeTypeMap());
            modelBuilder.Configurations.Add(new DbBackupMap());
            modelBuilder.Configurations.Add(new DepartmentRoleMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new LoginLogMap());
            modelBuilder.Configurations.Add(new ModuleButtonMapMap());
            modelBuilder.Configurations.Add(new ModuleMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new RoleModuleButtonMapMap());
            modelBuilder.Configurations.Add(new RoleModuleColumnMapMap());
            modelBuilder.Configurations.Add(new RoleModuleMapMap());
            modelBuilder.Configurations.Add(new RolePermissionMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new SysLogMap());
            modelBuilder.Configurations.Add(new UserPermissionMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserSettingMap());
        }
    }
}
