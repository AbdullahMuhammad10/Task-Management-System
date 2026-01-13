namespace Backend.Helper
{
    // Class To Handle Exception Responses
    public class ApiExceptionResponse : GeneralErrorResponse
    {
        // Additional Description For The Exception For Developer
        public string? Description { get; set; }
        public ApiExceptionResponse(int statusCode,string? message = null,string? description = null) : base(statusCode,message)
        {
            Description = description;
        }
    }
}
