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
            //var response = _db?.Shipments.Include(b => b.Carrier);

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
        public IActionResult Post([FromBody] ShipmentDTO model)
        {
            bool CarrierExists = _db!.Carrier!.Any(t => t.Name == model.CarrierName);
            bool CarrierServiceExists = _db!.CarrierService!.Any(t => t.Name == model.CarrierServiceName);

            Shipment model2 = new Shipment
            {
                width = model.width,
                height = model.height,
                weight = model.weight,
                CarrierId = 0,
                CarrierServiceId = 0
            };

            if (CarrierExists)
            {
                model2.CarrierId = _db.Carrier!.First(c => c.Name == model.CarrierName).Id;

                if (CarrierServiceExists)
                {
                    model2.CarrierServiceId = _db.CarrierService!.First(c => c.Name == model.CarrierServiceName).Id;

                }
                else
                {
                    return BadRequest("wrong CarrierService name");
                }
            }
            else
            {
                return BadRequest("wrong carrier name");
            }

            try
            {
                _db!.Shipments?.Add(model2);
                _db.SaveChanges();
                return Ok(model2);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }

}