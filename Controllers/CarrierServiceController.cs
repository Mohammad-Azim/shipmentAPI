using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentAPI.EfCore;
using ShipmentAPI.Model;

namespace ShipmentAPI.Controllers
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
            var response = _db?.CarrierService?.Include(b => b.Shipments);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CarrierService model)
        {
            try
            {
                _db!.CarrierService?.Add(model);
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