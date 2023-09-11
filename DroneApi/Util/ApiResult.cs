using System.Text.Json.Serialization;

namespace DroneApi.Util
{
    public class ApiResult
    {
        public string status { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string error_message { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string code_error { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public object data { get; set; }

        public static ApiResult Success(object data = null)
        {
            return new ApiResult
            {
                status = ApiResultStatus.Success,
                data = data
            };
        }

		public static ApiResult Error(string code_error, string message)
		{
			return new ApiResult
			{
				status = ApiResultStatus.Error,
				code_error = code_error,
				error_message = message
			};
		}

		public static ApiResult FatalError()
		{
			return new ApiResult
			{
				status = ApiResultStatus.Error,
				code_error = "fatal_error",
				error_message = "An unexpected error occurred"
			};
		}

		public static class ApiResultStatus
		{
			public const string Success = "success";
			public const string Error = "error";
		}
	}

	public class DataApiResponse
	{
		public int total { get; set; }
		public object items { get; set; }
		public DataApiResponse(int total, object data)
		{
			this.total = total;
			this.items = data;
		}
	}

}
