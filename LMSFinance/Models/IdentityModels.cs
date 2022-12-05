using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LMSFinance.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name"), Required]
        [StringLength(50)]
        public string FullName { get; set; }
        public string Gender { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public DbSet<Student> Students { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LMSFinance.Models.Subject> Subjects { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.Faculty> Faculties { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.StudyYear> StudyYears { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.SubjectDetail> SubjectDetails { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.SubmitForm> SubmitForms { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.Grade> Grades { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.Receipt> Receipts { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.Present> Presents { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.Discount> Discounts { get; set; }

        public System.Data.Entity.DbSet<LMSFinance.Models.Teacher> Teachers { get; set; }
    }
}