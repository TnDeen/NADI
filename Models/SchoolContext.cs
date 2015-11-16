using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVC5.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base("DefaultConnection")
        {
        }

        public DbSet <Student> Students { get; set; }
        public DbSet <Course> Courses { get; set; }
        public DbSet <Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}