using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{



    public void Configure(EntityTypeBuilder<Product> builder)
    {


    }
}
