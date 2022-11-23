using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentApi.EfCore;
using ShipmentApi.Model;

namespace shipmentAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CarrierController : ControllerBase
    {

        private EF_DataContext? _db;
        public CarrierController(EF_DataContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetCarrier")]
        public IActionResult Get()
        {
            var response = _db?.Carrier?.Include(b => b.Shipments).Include(e => e.CarrierServices);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Carrier model)
        {
            try
            {
                _db!.Carrier?.Add(model);
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