using Microsoft.EntityFrameworkCore;
using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
