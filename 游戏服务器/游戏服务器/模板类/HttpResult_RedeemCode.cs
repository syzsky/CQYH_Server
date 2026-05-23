namespace 游戏服务器.模板类
{
	public class HttpResult_RedeemCode
	{
		public int code { get; set; }

		public string message { get; set; }

		public HttpResult_RedeemCode_data data { get; set; }

		public int timestamp { get; set; }

		public string traceID { get; set; }
	}
}
