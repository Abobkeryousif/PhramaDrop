using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(n=> n.Name).IsRequired().HasMaxLength(50);
            builder.Property(p=> p.OldPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p=> p.NewPrice).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
