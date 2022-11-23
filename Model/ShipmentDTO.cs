

namespace ShipmentApi.Model
{
    public class ShipmentDTO
    {

        public int width { get; set; }

        public int height { get; set; }

        public int weight { get; set; }


        public string CarrierServiceName { get; set; } = string.Empty;

    }
}