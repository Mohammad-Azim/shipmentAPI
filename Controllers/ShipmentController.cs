using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentApi.EfCore;
using ShipmentApi.Model;

namespace shipmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipmentController : ControllerBase
    {
        private EF_DataContext? _db;
        public ShipmentController(EF_DataContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetShipments")]
        public IActionResult Get()
        {
            var response = from b in _db?.Shipments?.Include(b => b.Carrier).Include(s => s.CarrierService)
                           select new ShipmentDTO()
                           {
                               width = b.width,
                               height = b.height,
                               weight = b.weight,
                               CarrierName = b.Carrier!.Name!,
                               CarrierServiceName = b.CarrierService!.Name!
                           };
            return Ok(response);
        }

        [HttpPost(Name = "PostShipments")]
        public IActionResult Post([FromBody] ShipmentDTO modelDTO)
        {
            var carrier = _db!.Carrier?.Include(x => x.CarrierServices)!.SingleOrDefault(c => c.Name == modelDTO.CarrierName);

            Shipment model = new Shipment
            {
                width = modelDTO.width,
                height = modelDTO.height,
                weight = modelDTO.weight,
                CarrierId = 0,
                CarrierServiceId = 0
            };

            if (carrier != null)
            {
                model.CarrierId = carrier.Id;

                var carrierService = carrier.CarrierServices?.Find(t => t.Name == modelDTO.CarrierServiceName);

                if (carrierService != null)
                {
                    model.CarrierServiceId = carrierService.Id;
                }
                else
                {
                    return BadRequest("wrong CarrierService name 123");
                }
            }
            else
            {
                return BadRequest("wrong carrier name");
            }

            try
            {
                _db!.Shipments?.Add(model);
                _db.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }

}