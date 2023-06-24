namespace OCA.API.Models.Responses
{
    public class ApiResponse<T>
    {
        public DbResult<T> DbResult { get; set; } = new();
        public string RequestError { get; set; } = string.Empty;
        public bool IsValid { get; set; } = false;
        public int RequestStatusCode { get; set; }
    }
}
