namespace Fletes.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal DistanceKm { get; set; }

        public ICollection<Freight> Freights { get; set; }
    }
}
