namespace Backend.Helper
{
    public class GeneralErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public GeneralErrorResponse(int statusCode,string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessage(statusCode);
            ;
        }

        private string GetMessage(int statusCode)
        => statusCode switch
        {
            400 => "Bad Request — The server could not process your request due to client error.",
            404 => "Not Found — The requested resource could not be found.",
            500 => "Internal Server Error — An unexpected condition occurred on the server.",
            _ => "An unexpected error occurred. Please try again later."
        };
    }
}
