

namespace ShipmentAPI.Model
{
    public class ShipmentDTO
    {

        public int width { get; set; }

        public int height { get; set; }

        public int weight { get; set; }

        public string CarrierName { get; set; } = string.Empty;

        public string CarrierServiceName { get; set; } = string.Empty;

    }
}