namespace DroneApi.Util
{
    public class ApiResult
    {
        public ApiResult(string state, object data)
        {
            this.state = state;
            this.data = data;
        }

        public ApiResult(string state, string message_error)
        {
            this.state = state;
            this.message_error = message_error;
            this.data = data;
        }

        public string state { get; set; }
        public string message_error { get; set; }
        public object data { get; set; }
    }
}
