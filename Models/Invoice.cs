namespace Fletes.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int FreightId { get; set; }
        public Freight Freight { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
