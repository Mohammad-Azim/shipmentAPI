using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentAPI.Model;
using ShipmentAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ShipmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipmentController : ControllerBase
    {
        // private EF_DataContext? _db;

        private IUnitOfWork _unitOfWork;
        public ShipmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "admin")]
        [HttpGet(Name = "GetShipments")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await (from b in _unitOfWork?.Shipment?.GetAll()
                                      select new ShipmentDTO()
                                      {
                                          width = b.width,
                                          height = b.height,
                                          weight = b.weight,
                                          CarrierName = b.Carrier!.Name!,
                                          CarrierServiceName = b.CarrierService!.Name!
                                      }).ToListAsync();

                if (response.Count > 0)
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

        public async Task<IActionResult> Post([FromBody] ShipmentDTO modelDTO)
        {
            var carrier = await (_unitOfWork.Carrier.GetAll().Include(x => x.CarrierServices).FirstOrDefaultAsync(c => c.Name == modelDTO.CarrierName));

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
                    return BadRequest(ResponseHandler.GetAppResponse(ResponseType.NotFound, modelDTO, Info));
                }
            }
            else
            {
                string Info = $"Carrier {{{modelDTO.CarrierName}}} Not Found";
                return BadRequest(ResponseHandler.GetAppResponse(ResponseType.NotFound, modelDTO, Info));
            }

            try
            {

                _unitOfWork.Shipment.Add(model);
                await _unitOfWork.Save();
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


