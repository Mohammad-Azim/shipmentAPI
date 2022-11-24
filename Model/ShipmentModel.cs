using System.ComponentModel.DataAnnotations;

namespace ShipmentAPI.Model
{

    public class Shipment
    {
        [Key, Required]
        public int Id { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public int weight { get; set; }


        // Foreign Key
        public int CarrierId { get; set; }
        // Navigation property
        public Carrier? Carrier { get; set; }

        // Foreign Key
        public int CarrierServiceId { get; set; }
        // Navigation property
        public CarrierService? CarrierService { get; set; }

    }


}