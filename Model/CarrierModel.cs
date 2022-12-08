using System.ComponentModel.DataAnnotations;
namespace ShipmentAPI.Model
{

    public class Carrier
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public string dimentions_type { get; set; } = string.Empty;

        [Required]
        public string weight_type { get; set; } = string.Empty;

        // Navigation property
        public List<CarrierService>? CarrierServices { get; set; }

        // Navigation property
        public List<Shipment>? Shipments { get; set; }

    }
}