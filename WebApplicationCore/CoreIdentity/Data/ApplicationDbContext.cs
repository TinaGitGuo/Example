using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoreIdentity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Class1> Class1s { get; set; } // add this line code 
        public DbSet<Allowance> Allowances { get; set; }
        public class Class1
        {
            [Key]
            public int Class1ID { get; set; }
            public string Class1Name { get; set; }


            public string Id { get; set; } // because the ApplicationUser.id is nvarchar(450) , so this Id type is string;

            [ForeignKey("Id")]
            public virtual ApplicationUser User { get; set; }

        }
    }
}
