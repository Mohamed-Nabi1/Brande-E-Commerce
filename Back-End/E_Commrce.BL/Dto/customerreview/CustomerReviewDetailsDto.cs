namespace E_Commerce.BL;

public class CustomerReviewDetailsDto
{
    public string CustomerName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Rate { get; set; }
    public DateTime CreatedTime { get; set; }
}
