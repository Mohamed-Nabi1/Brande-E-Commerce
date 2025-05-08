namespace E_Commerce.DAL;

public class Customer : User
{

    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public Countries Country { get; set; }

    public Guid? WishListID { get; set; }
    public WishList? WishList { get; set; }
    public Guid? CartID { get; set; }
    public Cart? Cart { get; set; }
    public string? NameOnCard { get; set; } = string.Empty;
    public decimal? CardNumber { get; set; }
    public DateTime? ExpireDate { get; set; }

    public List<CustomerReview>? Reviews { get; set; }

    public List<Order>? Orders { get; set; } = null!;


}
