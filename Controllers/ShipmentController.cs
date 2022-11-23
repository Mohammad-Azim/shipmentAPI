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
            //var response = _db?.Shipments.Include(b => b.CarrierService);

            var response = from b in _db?.Shipments?.Include(b => b.CarrierService)
                           select new ShipmentDTO()
                           {
                               width = b.width,
                               height = b.height,
                               weight = b.weight,
                               CarrierServiceName = b.CarrierService!.Name!

                           };
            return Ok(response);
        }

        [HttpPost(Name = "PostShipments")]
        public IActionResult Post([FromBody] ShipmentDTO model)
        {

            bool CarrierServiceExists = _db!.CarrierServices!.Any(t => t.Name == model.CarrierServiceName);
            Shipment model2 = new Shipment
            {
                width = model.width,
                height = model.height,
                weight = model.weight,
                CarrierServiceId = 0
            };

            if (CarrierServiceExists)
            {
                model2.CarrierServiceId = _db.CarrierServices!.First(c => c.Name == model.CarrierServiceName).Id;
            }
            else
            {
                return BadRequest("wrong name");
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