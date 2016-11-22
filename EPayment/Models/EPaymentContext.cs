using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;


namespace EPayment.Models
{
    public class EPaymentContext :IdentityDbContext<AspNetUser>
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        // public DbSet<Position> Positions { get; set; }

        public DbSet<ExpensessType> ExpensessTypes { get; set; }
        public DbSet<Daily> Dailies { get; set; }
        public DbSet<DailyFile> DailyFiles { get; set; }
        public DbSet<DailyFileDetails> DailyFileDetailses { get; set; }
        public DbSet<Orgnization> Orgnizations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyBranch> CompanyBranches { get; set; }

        public EPaymentContext() : base("MyCon")
        {
            Database.SetInitializer<EPaymentContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static EPaymentContext Create()
        {
            return  new EPaymentContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
               modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        

            //ONE Department Many Employees
            modelBuilder.Entity<Department>()
                .HasMany(x => x.Employees)
                .WithOptional(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);


            //One Position Many Employees

            //modelBuilder.Entity<Position>()
            //   .HasMany(x => x.Employees)
            //   .WithOptional(x => x.Position)
            //   .HasForeignKey(x => x.PositionId);



            //One Expensess Many Daily

            modelBuilder.Entity<ExpensessType>().
                HasMany(x => x.Dailies)
                .WithRequired(x => x.ExpensessType)
                .HasForeignKey(x => x.ExpensessTypeId);


            //one Daily Many DailyFile
            modelBuilder.Entity<Daily>().HasMany(x => x.DailyFiles)
                .WithRequired(x => x.Daily).HasForeignKey(x => x.DailyId);



            //one DailyFile Many DailyFileDetails
            modelBuilder.Entity<DailyFile>()
                .HasMany(x => x.DailyFileDetailses)
                .WithRequired(x => x.DailyFile)
                .HasForeignKey(x => x.DailyFileId);


            //many Employees Many DailyFileDetails
            modelBuilder.Entity<Employee>()
                .HasMany(x => x.DailyFileDetails)
                .WithRequired(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);



            //one Company Many Branches
            modelBuilder.Entity<Company>()
                .HasMany(x => x.Branches)
                .WithRequired(x => x.Company)
                .HasForeignKey(x => x.CompanyId);
             

        }
    }

    public class AspNetUser : IdentityUser
    {
        public int Code { get; set; }
        public string NationalId { get; set; }
    }
}