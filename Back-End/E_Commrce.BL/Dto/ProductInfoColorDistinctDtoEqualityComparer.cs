using System.Diagnostics.CodeAnalysis;

namespace E_Commerce.BL;

public class ProductInfoColorDistinctDtoEqualityComparer : IEqualityComparer<ProductInfoColorDistinctDto>
{
    public bool Equals(ProductInfoColorDistinctDto x, ProductInfoColorDistinctDto y)
    {
        if (x == null || y == null)
            return false;

        return x.Color == y.Color;
    }

    public int GetHashCode(ProductInfoColorDistinctDto obj)
    {
        int hash = 15;
        hash = hash * 23 + obj.Color.GetHashCode();
        foreach (var sq in obj.SizeQuantities)
            hash = hash * 23 + sq.Quantity.GetHashCode() + sq.Size.GetHashCode();
        return hash;
    }
}
