namespace Fletes.Models
{
    public class FreightProduct
    {
        public int FreightId { get; set; }
        public Freight Freight { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
