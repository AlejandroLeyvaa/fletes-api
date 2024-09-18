namespace Fletes.Models.DTOs
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int FreightId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssueDate { get; set; }
    }

}
