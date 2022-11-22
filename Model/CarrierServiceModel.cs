using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShipmentApi.Model
{

    [Table("CarrierService")]
    public class CarrierService
    {
        [Key, Required]
        public int id { get; set; }

        [Required, StringLength(50)]
        public string? name { get; set; }

        [Required]
        public string dimentions_type { get; set; } = string.Empty;

        [Required]
        public string weight_type { get; set; } = string.Empty;

        public ICollection<Shipment>? shipments { get; set; }
    }
}