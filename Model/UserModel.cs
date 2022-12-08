using System.ComponentModel.DataAnnotations;

namespace ShipmentAPI.Model
{

    public class User
    {

        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        // Navigation property
        public List<Shipment>? Shipments { get; set; }

    }

}