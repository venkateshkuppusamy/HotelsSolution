namespace HotelsAPI.Models
{
    public class ErrorResult
    {
        public ErrorResult(string[] errors)
        {
            Errors = errors;
        }
        public string[] Errors { get; set; }
    }
}
