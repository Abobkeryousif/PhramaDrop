using Microsoft.EntityFrameworkCore;
using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
        }
       
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OTP> OTPs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<QRcode> QRcodes { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
