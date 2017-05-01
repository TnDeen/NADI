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
        [Display(Name = "Nama Penuh")]
        public string Nama { get; set; }
        public string Alamat { get; set; }
        [Display(Name = "No. Telefon Rumah")]
        public string NoTelRum { get; set; }
        [Display(Name = "No. Telefon Bimbit")]
        public string NoTelBim { get; set; }
        public string TempatLahir { get; set; }
        [Display(Name = "No. Kad Pengenalan")]
        public string NoPengenalan { get; set; }
        public string Bangsa { get; set; }
        public string Jantina { get; set; }
        public string Pekerjaan { get; set; }
        public string Jawatan { get; set; }
        [Display(Name = "Taraf Perkahwinan")]
        public string maritalStatus { get; set; }
        public DateTime? TarikhDaftarAhli { get; set; }
        public DateTime? TarikhSahAhli { get; set; }
        public DateTime? TarikhTamatAhli { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tarikh Penginapan")]
        public DateTime? tarikhPenginapan { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tarikh Lahir")]
        public DateTime? BirthDate { get; set; }

        public string NamaWaris { get; set; }
        public string AlamatWaris { get; set; }
        public string NomborTelefonWaris { get; set; }
        public string NomborTelefonWarisHP { get; set; }

        [Display(Name = "Nama Bank")]
        public string NamaBank { get; set; }
        [Display(Name = "Nombor Akaun Bank")]
        public string NomborAkaunBank { get; set; }

        [Required]
        public Boolean Perakuan { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual ApplicationUser Parent { get; set; }

        [ForeignKey("Negeri")]
        public int? NegeriId { get; set; }
        public virtual SAK Negeri { get; set; }

        [InverseProperty("Parent")]
        public ICollection<ApplicationUser> ChildList { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<FilePath> FilePaths { get; set; }
        public virtual ICollection<AffiliateComission> AffiliateComission { get; set; }

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

        public DbSet<SAK> Sak { get; set; }
        public DbSet<SK> Sk { get; set; }
        public DbSet<Message> SystemMessage { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<MemberPackage> MemberPackage { get; set; }
        public DbSet<MembershipRequest> MembershipRequest { get; set; }
        public DbSet<PosRequest> PosRequest { get; set; }
        public DbSet<AffiliateComission> AffiliateComission { get; set; }
        public DbSet<SistemId> SistemCounter { get; set; }
        public DbSet<Listing> Transactions { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}