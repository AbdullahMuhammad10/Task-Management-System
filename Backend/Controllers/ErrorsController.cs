namespace Backend.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    // To Ignore This Controller From Swagger Documentation.
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            var response = new GeneralErrorResponse(code);

            return code switch
            {
                400 => BadRequest(response),
                404 => NotFound(response),
                500 => StatusCode(500,response),
                _ => StatusCode(code,response)
            };
        }
    }
}
