namespace Fletes.Models
{
    public class Freight
    {
        public int FreightId { get; set; }
        public int CustomerId { get; set; }
        public int CarrierId { get; set; }
        public int VehicleId { get; set; }
        public int RouteId { get; set; }
        public Customer Customer { get; set; }
        public Carrier Carrier { get; set; }
        public Vehicle Vehicle { get; set; }
        public Route Route { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string Status { get; set; }

        public ICollection<FreightProduct> FreightProducts { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
