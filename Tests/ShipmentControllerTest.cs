using Xunit;
using ShipmentAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShipmentAPI.Model;
using ShipmentAPI.EfCore;

namespace ShipmentAPI.Test;
public class ShipmentControllerTest : ShipmentAPITestBase
{

    public bool AreEquals(ApiResponse obj, ApiResponse obj2)
    {
        return obj.MoreInfo == obj2.MoreInfo;
    }


    [Fact]
    public async void TestGetAllShipments()
    {
        var controller = new ShipmentController(_unitOfWork);

        var result = await controller.Get();

        var okResult = (result as OkObjectResult);

        Assert.NotNull(okResult?.Value);
    }



    [Fact]
    public async void TestPostShipment()
    {
        ShipmentDTO modelDTO = new ShipmentDTO
        {
            width = 20,
            height = 10,
            weight = 20,
            CarrierName = "fedex",
            CarrierServiceName = "fedexAIR"
        };


        var controller = new ShipmentController(_unitOfWork);

        var result = await controller.Post(modelDTO);

        var okResult = (result as OkObjectResult);

        string Info = $"Your shipment has been added to {{{modelDTO.CarrierName}}} Carrier Company In {{{modelDTO.CarrierServiceName}}} Carrier Service";
        var expected = controller.Ok(ResponseHandler.GetAppResponse(ResponseType.Success, modelDTO, Info));

        Assert.True(AreEquals((ApiResponse)okResult?.Value!, (ApiResponse)expected.Value!));
    }


    [Fact]
    public async void TestPostShipmentWrongCarrierName()
    {
        ShipmentDTO modelDTO = new ShipmentDTO
        {
            width = 20,
            height = 10,
            weight = 20,
            CarrierName = "fed", // not exist 
            CarrierServiceName = "fedexAIR"
        };


        var controller = new ShipmentController(_unitOfWork);

        var result = await controller.Post(modelDTO);

        var BadResult = (result as BadRequestObjectResult);

        string Info = $"Carrier {{{modelDTO.CarrierName}}} Not Found";
        var expected = controller.BadRequest(ResponseHandler.GetAppResponse(ResponseType.NotFound, modelDTO, Info));

        Assert.True(AreEquals((ApiResponse)BadResult!.Value!, (ApiResponse)expected.Value!));
    }

    [Fact]
    public async void TestPostShipmentWrongCarrierServiceName()
    {
        ShipmentDTO modelDTO = new ShipmentDTO
        {
            width = 20,
            height = 10,
            weight = 20,
            CarrierName = "fedex",
            CarrierServiceName = "fedexGround" // not exist 
        };


        var controller = new ShipmentController(_unitOfWork);

        var result = await controller.Post(modelDTO);

        var BadResult = (result as BadRequestObjectResult);

        string Info = $"The Carrier Service {{{modelDTO.CarrierServiceName}}} Not Found In {{{modelDTO.CarrierName}}} Carrier Company";
        var expected = controller.BadRequest(ResponseHandler.GetAppResponse(ResponseType.NotFound, modelDTO, Info));

        Assert.True(AreEquals((ApiResponse)BadResult?.Value!, (ApiResponse)expected.Value!));
    }
}