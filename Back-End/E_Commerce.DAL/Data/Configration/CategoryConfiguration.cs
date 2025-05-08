using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.DAL;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {

        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.HasMany(p => p.SubCategories)
            .WithOne(p => p.ParentCategory)
            .HasForeignKey(p => p.ParentCategoryId);
        builder.Property(p => p.Description).IsRequired();

    }
}
