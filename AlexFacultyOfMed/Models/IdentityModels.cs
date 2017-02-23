using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using AlexFacultyOfMed.Migrations;

namespace AlexFacultyOfMed.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.SqlServer;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AspNetUser : IdentityUser
    {
        public int Code { get; set; }
        public string NationalId { get; set; }

        public bool IsOnline { get; set; }

        public DateTime LastLoginDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            var claims = new List<Claim>();
            claims.Add(new Claim("CodeClaim",Code.ToString()));
            return userIdentity;
        }
    }
    [DbConfigurationType(typeof(DataContextConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<AspNetUser>
    {
        //  private readonly IHttpContextBaseWrapper _httpContextBaseWrapper;
        public ApplicationDbContext()
            : base("MyCon", false)
        {
            //  Database.SetInitializer<ApplicationDbContext>(new MigrateDatabaseToLatestVersion<ApplicationDbContext,Configuration>());
        }

        public DbSet<Employee> Employees { get; set; }
        //     public DbSet<Department> Departments { get; set; }
        // public DbSet<Position> Positions { get; set; }

     //   public DbSet<ExpensessType> ExpensessTypes { get; set; }
        public DbSet<Daily> Dailies { get; set; }
        public DbSet<DailyFile> DailyFiles { get; set; }
        public DbSet<DailyFileDetails> DailyFileDetailses { get; set; }
        public DbSet<DailyFileDetailsData> DailyFileDetailsDatas { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Complain> Complains { get; set; }

        public DbSet<ComplainDetails> ComplainDetailses { get; set; }

        public DbSet<Log> Logs { get; set; }

        // public DbSet<Orgnization> Orgnizations { get; set; }
        //  public DbSet<Company> Companies { get; set; }
        //  public DbSet<CompanyBranch> CompanyBranches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });


            //ONE Department Many Employees
            //modelBuilder.Entity<Department>()
            //    .HasMany(x => x.Employees)
            //    .WithOptional(x => x.Department)
            //    .HasForeignKey(x => x.DepartmentId);


            //One Position Many Employees

            //modelBuilder.Entity<Position>()
            //   .HasMany(x => x.Employees)
            //   .WithOptional(x => x.Position)
            //   .HasForeignKey(x => x.PositionId);


            //One Expensess Many Daily

            //modelBuilder.Entity<ExpensessType>().
            //    HasMany(x => x.Dailies)
            //    .WithRequired(x => x.ExpensessType)
            //    .HasForeignKey(x => x.ExpensessTypeId);


            //one Daily Many DailyFile
            modelBuilder.Entity<Daily>().HasMany(x => x.DailyFiles)
                .WithRequired(x => x.Daily).HasForeignKey(x => x.DailyId);


            //one DailyFile Many DailyFileDetails
            modelBuilder.Entity<DailyFile>()
                .HasMany(x => x.DailyFileDetailses)
                .WithRequired(x => x.DailyFile)
                .HasForeignKey(x => x.DailyFileId);

            //one DailyFile Many DailyFileDetails
            modelBuilder.Entity<DailyFileDetails>()
                .HasMany(x => x.DailyFileDetailsDatas)
                .WithRequired(x => x.DailyFileDetails)
                .HasForeignKey(x => x.DailyFileDetailsId);

            //many Employees Many DailyFileDetails
            modelBuilder.Entity<Employee>()
                .HasMany(x => x.DailyFileDetails)
                .WithRequired(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);

            //onew Complain Many ComplainDetails
            modelBuilder.Entity<Complain>()
                .HasMany(x => x.ComplainDetailses)
                .WithRequired(x => x.Complain)
                .HasForeignKey(x => x.ComplainId);

            modelBuilder.Entity<Account>()
                .HasMany(x => x.DailyFileDetailsDatas)
                .WithRequired(x => x.Account)
                .HasForeignKey(x => x.AccountId);
            //one Company Many Branches
            //modelBuilder.Entity<Company>()
            //    .HasMany(x => x.Branches)
            //    .WithRequired(x => x.Company)
            //    .HasForeignKey(x => x.CompanyId);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class DataContextConfiguration : DbConfiguration
    {
        public DataContextConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}