using Microsoft.AspNetCore.Mvc;
using ShipmentApi.EfCore;
using ShipmentApi.Model;

namespace shipmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrierServiceController : ControllerBase
    {
        private EF_DataContext? _db;
        public CarrierServiceController(EF_DataContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetCarrierServices")]
        public IActionResult Get()
        {
            var response = _db?.carrierServices.ToList();
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CarrierService model)
        {
            try
            {
                _db!.carrierServices.Add(model);
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