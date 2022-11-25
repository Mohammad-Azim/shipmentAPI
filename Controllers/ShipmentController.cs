using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentAPI.EfCore;
using ShipmentAPI.Model;

namespace ShipmentAPI.Controllers
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
            try
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

                if (response.Any())
                {
                    return Ok(response);
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }




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
                    string Info = $"The Carrier Service {{{modelDTO.CarrierServiceName}}} Not Found In {{{modelDTO.CarrierName}}} Carrier Company";
                    return Ok(ResponseHandler.GetAppResponse(ResponseType.NotFound, modelDTO, Info));
                }
            }
            else
            {
                string Info = $"Carrier {{{modelDTO.CarrierName}}} Not Found";
                return Ok(ResponseHandler.GetAppResponse(ResponseType.NotFound, modelDTO, Info));
            }

            try
            {
                _db!.Shipments?.Add(model);
                _db.SaveChanges();
                string Info = $"Your shipment has been added to {{{modelDTO.CarrierName}}} Carrier Company In {{{modelDTO.CarrierServiceName}}} Carrier Service";
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, modelDTO, Info));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }

}