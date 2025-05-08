using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(c => c.PaymentStatus).HasConversion<string>();
        builder.Property(c => c.PaymentMethod).HasConversion<string>();
        builder.Property(c => c.OrderStatus).HasConversion<string>();
        builder.Property(c => c.Country).HasConversion<string>();


    }
}
