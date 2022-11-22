using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipmentApi.Model
{

    [Table("Shipments")]
    public class Shipment
    {
        [Key, Required]
        public int id { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public int weight { get; set; }


        [Required]
        public int CarrierServiceId { get; set; }
        public CarrierService? carrierService { get; set; }
    }
}