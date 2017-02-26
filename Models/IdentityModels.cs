using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Net;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace MVC5.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public string NomborAhli { get; set; }
        public string HomeTown { get; set; }
        public string AccStatus { get; set; }
        public string Nama { get; set; }
        public DateTime? TarikhSahAhli { get; set; }
        public DateTime? TarikhTamatAhli { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tarikh Penginapan")]
        public DateTime? tarikhPenginapan { get; set; }

        public DateTime? BirthDate { get; set; }

        public string NamaWaris { get; set; }
        public string NomborTelefonWaris { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual ApplicationUser Parent { get; set; }

        [InverseProperty("Parent")]
        public ICollection<ApplicationUser> ChildList { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<FilePath> FilePaths { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<SistemId> SistemCounter { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}