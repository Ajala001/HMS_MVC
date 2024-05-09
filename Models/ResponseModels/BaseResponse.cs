namespace HMSMVC.Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
