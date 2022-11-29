using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentAPI.Interfaces;
using ShipmentAPI.Model;

namespace ShipmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _unitOfWork.User.GetAll().ToListAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                _unitOfWork.User.Add(user);
                var response = await Task.FromResult(user);
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, response));

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


    }
}