namespace Fletes.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal WeightKg { get; set; }
        public decimal VolumeM3 { get; set; }

        public ICollection<FreightProduct> FreightProducts { get; set; }
    }
}
