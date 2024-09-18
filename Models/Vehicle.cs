namespace Fletes.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int IsAvailable { get; set; }
        public string Status { get; set; }

        public ICollection<Freight> Freights { get; set; }
    }
}
