using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentAPI.Interfaces;
using ShipmentAPI.Model;

namespace ShipmentAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CarrierServiceController : ControllerBase
    {

        private IUnitOfWork? _unitOfWork;
        public CarrierServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetCarrierServices")]
        public IActionResult Get()
        {
            var response = _unitOfWork?.CarrierService?.GetAll().Include(b => b.Shipments);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarrierService model)
        {
            try
            {
                _unitOfWork!.CarrierService?.Add(model);
                await _unitOfWork.Save();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }


}