namespace Fletes.Models
{
    public class Carrier
    {
        public int CarrierId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Freight> Freights { get; set; }
    }
}
