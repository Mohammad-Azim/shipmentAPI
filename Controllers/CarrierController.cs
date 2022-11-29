using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentAPI.Interfaces;
using ShipmentAPI.Model;

namespace ShipmentAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CarrierController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        public CarrierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetCarrier")]
        public IActionResult Get()
        {
            var response = _unitOfWork?.Carrier?.GetAll().Include(b => b.Shipments).Include(e => e.CarrierServices);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Carrier model)
        {
            try
            {
                _unitOfWork.Carrier.Add(model);
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }


}