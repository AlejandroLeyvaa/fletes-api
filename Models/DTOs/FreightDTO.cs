namespace Fletes.Models.DTOs
{
    public class FreightDTO
    {
        public int FreightId { get; set; }
        public int CustomerId { get; set; }
        public int CarrierId { get; set; }
        public int VehicleId { get; set; }
        public int RouteId { get; set; }
        public virtual CustomerDTO Customer { get; set; }

        public virtual CarrierDTO Carrier { get; set; }

        public virtual VehicleDTO Vehicle { get; set; }

        public virtual RouteDTO Route { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string Status { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
