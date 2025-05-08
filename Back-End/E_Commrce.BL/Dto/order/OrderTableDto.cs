namespace E_Commerce.BL
{
    public class OrderTableDto
    {
        public Guid OrderId { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
