namespace User_Authentication_System.Helpers
{
    public class ResponseData <T> where T : class
    {
        public T? Data { get; set; }

        public string? Errors { get; set; }
    }
}
