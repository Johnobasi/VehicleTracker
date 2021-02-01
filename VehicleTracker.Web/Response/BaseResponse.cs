namespace VehicleTracker.Web.Response
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public string Code { get; set; }
        public T Body { get; set; }
    }
}
