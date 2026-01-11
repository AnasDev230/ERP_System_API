using ERP_System.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERP_System.Data
{
    public class SystemDbContext:IdentityDbContext<IdentityUser>
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            var adminRoleId = "3F78BEC5-91CF-40D6-A996-2DAFFFBA971A";
            var accountantRoleId = "F2A6879F-EDA2-4E5A-BAAF-EB14DDE95C10";
            var salesRoleId = "039C8585-9C83-4294-9490-494A150514B6";
            var Roles = new List<IdentityRole>
    {
         new IdentityRole
        {
            Id = adminRoleId,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
         new IdentityRole
        {
            Id = accountantRoleId,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Name = "Accountant",
            NormalizedName = "ACCOUNTANT"
        },
          new IdentityRole
        {
            Id = salesRoleId,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Name = "Sales",
            NormalizedName = "SALES"
        },


    };
            modelBuilder.Entity<IdentityRole>().HasData(Roles);





        }
        }
}
