using System.ComponentModel.DataAnnotations;
namespace ShipmentAPI.Model
{

    public class CarrierService
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? Name { get; set; }

        public int CarrierId { get; set; }


        // Navigation property
        public List<Shipment>? Shipments { get; set; }

    }
}