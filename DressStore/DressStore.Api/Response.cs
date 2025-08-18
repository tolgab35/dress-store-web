namespace DressStore.Api
{
    public class Response<T>
    {
        public T data { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
    }
}
