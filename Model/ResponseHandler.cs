namespace ShipmentApi.Model
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception ex)
        {
            ApiResponse response = new ApiResponse();
            response.Code = "500";
            response.ResponseData = ex.Message;
            return response;
        }
        public static ApiResponse GetAppResponse(ResponseType type, object? contract, string MoreInfo = "")
        {
            ApiResponse response;

            response = new ApiResponse { ResponseData = contract };
            switch (type)
            {
                case ResponseType.Success:
                    response.Code = "200";
                    response.MoreInfo = MoreInfo;
                    response.Message = "Success";

                    break;
                case ResponseType.NotFound:
                    response.Code = "404";
                    response.MoreInfo = MoreInfo;
                    response.Message = "No record available";
                    break;
            }
            return response;
        }
    }
}
