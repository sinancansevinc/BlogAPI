namespace Net7Basic.Dtos
{
    public class ResponseDto<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }

    }
}
