namespace Backend.Helper
{
    // Class To Handle Validation Error Responses
    public class ApiValidationErrorResponse : GeneralErrorResponse
    {
        // List Of Errors Occurred During Validation
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }
    }
}
